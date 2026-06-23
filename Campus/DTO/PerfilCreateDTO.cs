using System.ComponentModel.DataAnnotations;

namespace Campus.DTO
{
    public class PerfilCreateDTO
    {
        [Required]
        public string NombrePerfil { get; set; }
        [Required]
        public string Descripcion { get; set; }
        public string[] lenguajes { get; set; }
    }
}
