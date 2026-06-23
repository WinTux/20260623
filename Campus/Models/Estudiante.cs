using System.ComponentModel.DataAnnotations;

namespace Campus.Models
{
    public class Estudiante
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int fId { get; set; }
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
        public string Direccion { get; set; }
        public ICollection<Perfil> perfiles { get; set; } = new List<Perfil>();
    }
}
