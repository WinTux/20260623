using AutoMapper;
using GestionEstudiantes.ComunicacionAsync;
using GestionEstudiantes.ComunicacionSync.http;
using GestionEstudiantes.DTO;
using GestionEstudiantes.Models;
using GestionEstudiantes.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace GestionEstudiantes.Controllers
{
    [Route("api/[controller]")] // http://localhost:5000/api/estudiante
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        private readonly IEstudianteRepository repo;
        private readonly IMapper mapper;
        private readonly ICampusHistorialEstudiante campusHistorialEstudiante;
        private readonly IBusDeMensajesCliente busDeMensajesCliente;

        public EstudianteController(IEstudianteRepository repo, IMapper mapper, ICampusHistorialEstudiante campusHistorialEstudiante, IBusDeMensajesCliente busDeMensajesCliente)
        {
            this.repo = repo;
            this.mapper = mapper;
            this.campusHistorialEstudiante = campusHistorialEstudiante;
            this.busDeMensajesCliente = busDeMensajesCliente;
        }
        [HttpGet]
        public ActionResult<IEnumerable<EstudianteReadDTO>> GetEstudiantes()
        {
            var estudiantes = repo.GetAllEstudiantes();
            return Ok(mapper.Map<IEnumerable<EstudianteReadDTO>>(estudiantes));
        }
        [HttpGet("{id}", Name = "GetEstudianteById")]
        public ActionResult<EstudianteReadDTO> GetEstudianteById(int id)
        {
            var estudiante = repo.GetEstudianteById(id);
            if (estudiante == null)
                return NotFound();
            return Ok(mapper.Map<EstudianteReadDTO>(estudiante));
        }
        [HttpPost]
        public async Task<ActionResult<EstudianteReadDTO>> CreateEstudiante(EstudianteCreateDTO estudianteCreateDTO)
        {
            var estudianteModel = mapper.Map<Estudiante>(estudianteCreateDTO);
            repo.AddEstudiante(estudianteModel);
            repo.Guardar();
            var estudianteReadDTO = mapper.Map<EstudianteReadDTO>(estudianteModel);
            try
            {
                await campusHistorialEstudiante.ComunicarseConCampus(estudianteReadDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"No se pudo comunicar con el servicio externo Campus: {ex.Message}");
            }
            try
            {
                var estudianteProducerDTO = mapper.Map<EstudianteProducerDTO>(estudianteReadDTO);
                estudianteProducerDTO.tipoEvento = "estudiante_publicado";
                busDeMensajesCliente.PublicarNuevoEstudiante(estudianteProducerDTO);
            }catch(Exception ex) { Console.WriteLine("Ocurrió un error al tratar de publicar: "+ex.ToString()); }
            return CreatedAtRoute(nameof(GetEstudianteById), new { id = estudianteModel.Id }, estudianteReadDTO);
        }
        [HttpPut("{id}")]
        public ActionResult UpdateEstudiante(int id, EstudianteUpdateDTO estudianteUpdateDTO)
        {
            var estudianteModelFromRepo = repo.GetEstudianteById(id);
            if (estudianteModelFromRepo == null)
                return NotFound();
            mapper.Map(estudianteUpdateDTO, estudianteModelFromRepo);
            repo.UpdateEstudiante(estudianteModelFromRepo);
            repo.Guardar();
            return NoContent();
        }
        [HttpPatch("{id}")]
        public ActionResult PartialEstudianteUpdate(int id, JsonPatchDocument<EstudianteUpdateDTO> patchDoc)
        {
            var estudianteModelFromRepo = repo.GetEstudianteById(id);
            if (estudianteModelFromRepo == null)
                return NotFound();
            var estudianteToPatch = mapper.Map<EstudianteUpdateDTO>(estudianteModelFromRepo);
            patchDoc.ApplyTo(estudianteToPatch, ModelState);
            if (!TryValidateModel(estudianteToPatch))
            {
                return ValidationProblem(ModelState);
            }
            mapper.Map(estudianteToPatch, estudianteModelFromRepo);
            repo.UpdateEstudiante(estudianteModelFromRepo);
            repo.Guardar();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteEstudiante(int id)
        {
            var estudianteModelFromRepo = repo.GetEstudianteById(id);
            if (estudianteModelFromRepo == null)
                return NotFound();
            repo.DeleteEstudianteById(estudianteModelFromRepo);
            repo.Guardar();
            return NoContent();
        }
    }
}
