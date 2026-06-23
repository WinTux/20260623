using System.ComponentModel.DataAnnotations;

namespace GestionEstudiantes.DTO
{
    public class EstudianteCreateDTO
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }
}
