using AutoMapper;
using Campus.Conexion;
using Campus.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Campus.Controllers
{
    [ApiController]
    [Route("api/campus/[controller]")]
    public class EstudianteController : ControllerBase
    {
        private readonly IPerfilRepository _repo;
        private readonly IMapper _mapper;
        public EstudianteController(IPerfilRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<EstudianteReadDTO>> GetEstudiantes()
        {
            Console.WriteLine("Se obtienen estudiantes desde servicio Campus.");
            var estudiantes = _repo.GetEstudiantes();
            return Ok(_mapper.Map<IEnumerable<EstudianteReadDTO>>(estudiantes));
        }
    }
}
