using GestionEstudiantes.Repositories;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Newtonsoft.Json.Serialization;
using GestionEstudiantes.ComunicacionSync.http;
using GestionEstudiantes.ComunicacionAsync;

namespace GestionEstudiantes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<InstitutoDbContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("una_canexion")));
            builder.Services.AddHttpClient<ICampusHistorialEstudiante,ImplHttpCampusHistorialEstudiante>();
            builder.Services.AddAuthorization();
            builder.Services.AddControllers()
                .AddNewtonsoftJson(s => 
                s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddScoped<IEstudianteRepository, ImplEstudianteRepository>();
            builder.Services.AddSingleton<IBusDeMensajesCliente,ImplBusDeMensajesCliente>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseRouting();
            app.MapControllers();

            app.Run();
        }
    }
}
