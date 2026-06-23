using GestionEstudiantes.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionEstudiantes.Repositories
{
    public class InstitutoDbContext : DbContext
    {
        public DbSet<Estudiante> Estudiantes { get; set; }
        public InstitutoDbContext(DbContextOptions<InstitutoDbContext> options) : base(options)
        {
        }
    }
}
