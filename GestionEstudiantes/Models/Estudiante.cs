using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionEstudiantes.Models
{
    [Table("Estudiante")]
    public class Estudiante
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }
        [Required]
        [MaxLength(50)]
        public string Apellido { get; set; }
        [Required]
        public DateTime FechaNacimiento { get; set; }
        [MaxLength(150)]
        public string? Email { get; set; }
    }
}
