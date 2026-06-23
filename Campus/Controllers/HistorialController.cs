using Microsoft.AspNetCore.Mvc;

namespace Campus.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistorialController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post()
        {
            Console.WriteLine("Llegó una petición al servicio Campus - HistorialController");
            return Ok("Llegó una petición al servicio Campus - HistorialController");
        }
    }
}
