using Campus.Models;

namespace Campus.Conexion
{
    public interface IPerfilRepository
    {
        // Para estudiantes
        IEnumerable<Estudiante> GetEstudiantes();
        void CrearEstudiante(Estudiante estudiante);
        bool ExisteEstudiante(int id);
        bool ExisteEstudianteForaneo(int fid);

        //Para perfiles
        Perfil GetPerfil(int idPerfil, int id);
        IEnumerable<Perfil> GetPerfiles(int id);
        void CrearPerfil(int id, Perfil perfil);

        // General
        bool Guardar();
    }
}
