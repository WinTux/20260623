using System.ComponentModel.DataAnnotations;

namespace Campus.Models
{
    public class Perfil
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string NombrePerfil { get; set; }
        [Required]
        public string Descripcion { get; set; }
        public string lenguajes { get; set; }
        public int estudianteId { get; set; }
        public Estudiante estudiante { get; set; }
    }
}
