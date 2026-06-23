using GestionEstudiantes.Models;

namespace GestionEstudiantes.Repositories
{
    public class ImplEstudianteRepository : IEstudianteRepository
    {
        private readonly InstitutoDbContext context;
        public ImplEstudianteRepository(InstitutoDbContext context)
        {
            this.context = context;
        }

        public bool AddEstudiante(Estudiante estudiante)
        {
            if(estudiante == null)
                throw new ArgumentNullException(nameof(estudiante));
            context.Estudiantes.Add(estudiante);
            return true;
        }

        public void DeleteEstudianteById(Estudiante estudiante)
        {
            if(estudiante == null)
                throw new ArgumentNullException(nameof(estudiante));
            context.Estudiantes.Remove(estudiante);
        }

        public IEnumerable<Estudiante> GetAllEstudiantes()
        {
            IEnumerable<Estudiante> estudiantes = context.Estudiantes.ToList();
            return estudiantes;
        }

        public Estudiante GetEstudianteById(int id)
        {
            return context.Estudiantes.FirstOrDefault(e => e.Id == id);
        }

        public bool Guardar()
        {
            context.SaveChanges();
            return true;
        }

        public void UpdateEstudiante(Estudiante estudiante)
        {
            // No implementation needed for EF Core as it tracks changes automatically
        }
    }
}
