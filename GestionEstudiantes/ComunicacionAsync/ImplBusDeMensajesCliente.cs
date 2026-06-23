using GestionEstudiantes.DTO;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace GestionEstudiantes.ComunicacionAsync
{
    public class ImplBusDeMensajesCliente : IBusDeMensajesCliente
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection connection;
        private readonly RabbitMQ.Client.IModel channel;
        public void RabbitMQ_evento_shutdown(object sender, ShutdownEventArgs args) {
            Console.WriteLine("Se desconecta de RabbitMQ y podríamos ejecutar algo acá.");
        }
        public ImplBusDeMensajesCliente(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionFactory factory = new ConnectionFactory() { 
                HostName = _configuration["Host_RabbitMQ"],
                Port = int.Parse(_configuration["Puerto_RabbitMQ"])
            };
            try
            {
                connection = factory.CreateConnection();
                channel = connection.CreateModel();
                channel.ExchangeDeclare(
                       exchange: "mi_exchange",
                       type: ExchangeType.Fanout
                );
                connection.ConnectionShutdown += RabbitMQ_evento_shutdown;
            }
            catch (Exception ex) {
                Console.WriteLine($"Error al tratar de establecer la publicación de un estudiante al bus RabbitMQ: {ex.Message}");
            }
        }
        public void PublicarNuevoEstudiante(EstudianteProducerDTO estudiante)
        {
            string mensaje = JsonSerializer.Serialize(estudiante);
            if (connection.IsOpen)
                Enviar(mensaje);
            else
                Console.WriteLine("No se pudo enviar el mensaje al bus de mensajes RabbitMQ");
        }

        private void Enviar(string mensaje)
        {
            var cuerpo = Encoding.UTF8.GetBytes(mensaje);
            channel.BasicPublish(
                exchange: "mi_exchange",
                routingKey: "",
                basicProperties: null,
                body: cuerpo
            );
            Console.WriteLine("Se envió el mensaje al bus de mensajes RabbitMQ");
        }
        private void Finalizar() {
            if (channel.IsOpen) {
                channel.Close();
                connection.Close();
            }
        }
    }
}
