using System.ComponentModel.DataAnnotations;

namespace GestionEstudiantes.DTO
{
    public class EstudianteUpdateDTO
    {
        public DateTime FechaNacimiento { get; set; }
        public string? Email { get; set; }
    }
}
