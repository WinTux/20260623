using AutoMapper;
using Campus.DTO;
using System.Text.Json;

namespace Campus.Eventos
{
    public class ProcesadorDeEventos : IProcesadorDeEventos
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper mapper;
        public ProcesadorDeEventos(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            this.mapper = mapper;
        }
        public void ProcesarEvento(string mensaje)
        {
            var tipo = DeterminarTipoEvento(mensaje);
            switch (tipo)
            {
                case TipoEvento.NuevoPerfil:
                    //
                    break;
                case TipoEvento.EliminarPerfil:
                    //EliminarPerfil(mensaje);
                    break;
                case TipoEvento.estudiante_publicado:
                    AgregarEstudiante(mensaje);
                    break;
                default:
                    break;
            }
        }

        private TipoEvento DeterminarTipoEvento(string mensaje)
        {
            EventoDTO tipo = JsonSerializer.Deserialize<EventoDTO>(mensaje);
            switch (tipo.evento)
            {
                case "NuevoPerfil":
                    return TipoEvento.NuevoPerfil;
                case "EliminarPerfil":
                    return TipoEvento.EliminarPerfil;
                case "estudiante_publicado":
                    return TipoEvento.estudiante_publicado;
                default:
                    return TipoEvento.desconocido;
            }
        }
        private void AgregarEstudiante(string mensaje)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<Conexion.IPerfilRepository>();
                var estudianteProducerDTO = JsonSerializer.Deserialize<EstudianteProducerDTO>(mensaje);
                try
                {
                    var estudiante = mapper.Map<Models.Estudiante>(estudianteProducerDTO);
                    if (!repo.ExisteEstudianteForaneo(estudiante.fId))
                    {
                        repo.CrearEstudiante(estudiante);
                        repo.Guardar();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al agregar estudiante: {ex.Message}");
                }
            }
        }
    }
    enum TipoEvento
    {
        NuevoPerfil,
        EliminarPerfil,
        estudiante_publicado,
        desconocido
    }
}
