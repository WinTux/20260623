using Microsoft.EntityFrameworkCore;

namespace Campus.Conexion
{
    public class CampusDbContext : DbContext
    {
        public DbSet<Models.Estudiante> Estudiantes { get; set; }
        public DbSet<Models.Perfil> Perfiles { get; set; }
        public CampusDbContext(DbContextOptions<CampusDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Estudiante>()
                .HasMany(e => e.perfiles)
                .WithOne(p => p.estudiante)
                .HasForeignKey(p => p.estudianteId);
            modelBuilder.Entity<Models.Perfil>()
                .HasOne(p => p.estudiante)
                .WithMany(e => e.perfiles)
                .HasForeignKey(p => p.estudianteId);
        }
    }
}
