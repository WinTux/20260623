using GestionEstudiantes.DTO;

namespace GestionEstudiantes.ComunicacionSync.http
{
    public class ImplHttpCampusHistorialEstudiante : ICampusHistorialEstudiante
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public ImplHttpCampusHistorialEstudiante(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task ComunicarseConCampus(EstudianteReadDTO est)
        {
            StringContent cuerpoHttp = new StringContent(System.Text.Json.JsonSerializer.Serialize(est), System.Text.Encoding.UTF8, "application/json");
            var respuesta = _httpClient.PostAsync($"{_configuration["ServiciosExternos"]}/api/Historial", cuerpoHttp).Result;
            if (respuesta.IsSuccessStatusCode)
            {
                Console.WriteLine("Éxito al comunicarse con el servicio externo Campus por POST.");
            }
            else
            {
                //throw new Exception("Error al comunicarse con el servicio externo.");
                Console.WriteLine("Error al comunicarse con el servicio externo Campus por POST.");
            }
        }
    }
}
