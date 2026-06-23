using GestionEstudiantes.Models;

namespace GestionEstudiantes.Repositories
{
    public interface IEstudianteRepository
    {
        IEnumerable<Estudiante> GetAllEstudiantes();
        Estudiante GetEstudianteById(int id);
        bool AddEstudiante(Estudiante estudiante);
        void UpdateEstudiante(Estudiante estudiante);
        void DeleteEstudianteById(Estudiante estudiante);
        bool Guardar();
    }
}
