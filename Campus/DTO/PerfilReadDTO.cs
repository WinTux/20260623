using System.ComponentModel.DataAnnotations;

namespace Campus.DTO
{
    public class PerfilReadDTO
    {
        public int Id { get; set; }
        public string NombrePerfil { get; set; }
        public string Descripcion { get; set; }
        public string lenguajes { get; set; }
        public int estudianteId { get; set; }
    }
}
