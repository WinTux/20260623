using Campus.Models;

namespace Campus.Conexion
{
    public class ImplPerfilRepository : IPerfilRepository
    {
        private readonly CampusDbContext _context;
        public ImplPerfilRepository(CampusDbContext context)
        {
            _context = context;
        }
        // Para estudiantes
        public IEnumerable<Estudiante> GetEstudiantes()
        {
            return _context.Estudiantes.ToList();
        }
        public void CrearEstudiante(Estudiante estudiante)
        {
            if(estudiante == null)
            {
                throw new ArgumentNullException(nameof(estudiante));
            }
            else
            {
                _context.Estudiantes.Add(estudiante);
            }
        }

        public bool ExisteEstudiante(int id)
        {
            return _context.Estudiantes.Any(e => e.Id == id);// SELECT * FROM Estudiantes WHERE Id = id
        }

        // Para perfiles

        public Perfil GetPerfil(int idPerfil, int id)
        {
            return _context.Perfiles.Where(p => p.Id == idPerfil && p.estudianteId == id).FirstOrDefault();
        }

        public IEnumerable<Perfil> GetPerfiles(int id)
        {
            return _context.Perfiles.Where(p => p.estudianteId == id).ToList();
        }
        public void CrearPerfil(int id, Perfil perfil)
        {
            if(perfil == null)
            {
                throw new ArgumentNullException(nameof(perfil));
            }
            else
            {
                perfil.estudianteId = id;
                _context.Perfiles.Add(perfil);
            }
        }
        // General
        public bool Guardar()
        {
            return (_context.SaveChanges() >= 0);
        }

        public bool ExisteEstudianteForaneo(int fid)
        {
            return _context.Estudiantes.Any(e => e.fId == fid);
        }
    }
}
