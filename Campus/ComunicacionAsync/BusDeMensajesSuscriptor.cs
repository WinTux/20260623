using Campus.Eventos;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Threading.Channels;

namespace Campus.ComunicacionAsync
{
    public class BusDeMensajesSuscriptor : BackgroundService
    {
        private readonly IProcesadorDeEventos _procesadorDeEventos;
        private readonly IConfiguration configuration;
        private IConnection _connection;
        private IModel _channel;
        private string cola;
        public BusDeMensajesSuscriptor(IProcesadorDeEventos procesadorDeEventos, IConfiguration configuration)
        {
            _procesadorDeEventos = procesadorDeEventos;
            this.configuration = configuration;
            IniciarRabbitMQ();
        }

        private void IniciarRabbitMQ()
        {
            var factory = new ConnectionFactory()
            {
                HostName = configuration["Host_RabbitMQ"],
                Port = int.Parse(configuration["Puerto_RabbitMQ"])
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "mi_exchange", type: ExchangeType.Fanout);
            cola = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queue: cola,
                               exchange: "mi_exchange",
                               routingKey: "");
            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        }
        private void RabbitMQ_ConnectionShutdown(object? sender, ShutdownEventArgs e)
        {
            Console.WriteLine("Conexion cerrada");
        }
        public override void Dispose()
        {
            if(_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
            base.Dispose();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumidor = new EventingBasicConsumer(_channel);
            consumidor.Received += (ModuleHandle, ea) =>
            {
                Console.WriteLine("Evento recibido");
                var cuerpo = ea.Body;
                var mensaje = System.Text.Encoding.UTF8.GetString(cuerpo.ToArray());
                _procesadorDeEventos.ProcesarEvento(mensaje);
            };
            _channel.BasicConsume(queue: cola,
                                 autoAck: true,
                                 consumer: consumidor);
            return Task.CompletedTask;
        }
    }
}
