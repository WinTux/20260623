using Campus.Conexion;
using Campus.Eventos;
using Microsoft.EntityFrameworkCore;

namespace Campus
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddDbContext<CampusDbContext>(op => op.UseInMemoryDatabase("miDb"));
            builder.Services.AddScoped<IPerfilRepository, ImplPerfilRepository>();
            builder.Services.AddSingleton<IProcesadorDeEventos, ProcesadorDeEventos>();
            builder.Services.AddHostedService<ComunicacionAsync.BusDeMensajesSuscriptor>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
