using AutoMapper;
using Campus.Conexion;
using Campus.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Campus.Controllers
{
    [Route("api/campus/[controller]")]
    [ApiController]
    public class PerfilController : ControllerBase
    {
        private readonly IPerfilRepository _repo;
        private readonly IMapper _mapper;
        public PerfilController(IPerfilRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<PerfilReadDTO>> GetPerfiles(int estudianteid)
        {
            Console.WriteLine($"Se obtienen perfiles del estudiante {estudianteid}, desde servicio Campus.");
            if(!_repo.ExisteEstudiante(estudianteid))
                return NotFound();
            var perfiles = _repo.GetPerfiles(estudianteid);
            return Ok(_mapper.Map<IEnumerable<PerfilReadDTO>>(perfiles));
        }
        [HttpGet("{perfilid}", Name ="GetPerfilDeEstudiante")]
        public ActionResult<PerfilReadDTO> GetPerfilDeEstudiante(int estudianteid, int perfilid)
        {
            Console.WriteLine($"Se obtiene el perfil {perfilid} del estudiante {estudianteid}, desde servicio Campus.");
            if(!_repo.ExisteEstudiante(estudianteid))
                return NotFound();
            var perfil = _repo.GetPerfil(perfilid, estudianteid);
            if(perfil == null)
                return NotFound();
            return Ok(_mapper.Map<PerfilReadDTO>(perfil));
        }
        [HttpPost]
        public ActionResult<PerfilReadDTO> CrearPerfilDeEstudiante(int estudianteid, PerfilCreateDTO perfilCreateDTO)
        {
            Console.WriteLine($"Se crea un nuevo perfil para el estudiante {estudianteid}, desde servicio Campus.");
            if(!_repo.ExisteEstudiante(estudianteid))
                return NotFound();
            var perfilModel = _mapper.Map<Campus.Models.Perfil>(perfilCreateDTO);
            _repo.CrearPerfil(estudianteid, perfilModel);
            _repo.Guardar();
            var perfilReadDTO = _mapper.Map<PerfilReadDTO>(perfilModel);
            return CreatedAtRoute(nameof(GetPerfilDeEstudiante), new { estudianteid = estudianteid, perfilid = perfilReadDTO.Id }, perfilReadDTO);
        }
    }
}
