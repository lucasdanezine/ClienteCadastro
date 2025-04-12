using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClienteCadastro.Infrastructure.Consumers
{
    public class ClienteCriadoConsumer : BackgroundService
    {
        private readonly ILogger<ClienteCriadoConsumer> _logger;
        private readonly IConfiguration _configuration;

        private IConnection? _connection;
        private IModel? _channel;

        public ClienteCriadoConsumer(ILogger<ClienteCriadoConsumer> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQ:HostName"],
                Port = int.Parse(_configuration["RabbitMQ:Port"] ?? "5672"),
                UserName = _configuration["RabbitMQ:UserName"],
                Password = _configuration["RabbitMQ:Password"]
            };

            int maxRetries = 5;
            for (int i = 0; i < maxRetries; i++)
            {
                try
                {
                    _connection = factory.CreateConnection();
                    _channel = _connection.CreateModel();

                    _channel.QueueDeclare(queue: "cliente-criado",
                                          durable: false,
                                          exclusive: false,
                                          autoDelete: false,
                                          arguments: null);

                    _logger.LogInformation("✅ Conectado ao RabbitMQ com sucesso.");
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogWarning("❌ Falha ao conectar no RabbitMQ (tentativa {Tentativa}): {Erro}", i + 1, ex.Message);
                    Thread.Sleep(3000); // aguarda 3s antes da próxima tentativa
                    if (i == maxRetries - 1) throw;
                }
            }

            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel!);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                _logger.LogInformation($"📨 Mensagem recebida da fila 'cliente-criado': {message}");

                // processar a mensagem aqui
            };

            _channel!.BasicConsume(queue: "cliente-criado",
                                  autoAck: true,
                                  consumer: consumer);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
            base.Dispose();
        }
    }
}
