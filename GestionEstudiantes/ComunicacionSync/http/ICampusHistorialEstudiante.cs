using GestionEstudiantes.DTO;

namespace GestionEstudiantes.ComunicacionSync.http
{
    public interface ICampusHistorialEstudiante
    {
        Task ComunicarseConCampus(EstudianteReadDTO est);
    }
}
