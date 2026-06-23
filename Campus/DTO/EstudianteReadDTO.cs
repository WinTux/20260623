using Campus.Models;
using System.ComponentModel.DataAnnotations;

namespace Campus.DTO
{
    public class EstudianteReadDTO
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string? Email { get; set; }
        public string Direccion { get; set; }
    }
}
