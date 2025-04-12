using ClienteCadastro.Domain.Entities;
using ClienteCadastro.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace ClienteCadastro.Infrastructure.Services;

public class RabbitMqService : IRabbitMqService
{
    private readonly IConfiguration _configuration;

    public RabbitMqService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void PublicarClienteCriado(Cliente cliente)
    {
        var factory = new ConnectionFactory
        {
            HostName = _configuration["RabbitMq:Host"] ?? "localhost",
            Port = int.Parse(_configuration["RabbitMq:Port"] ?? "5672"),
            UserName = _configuration["RabbitMq:Username"] ?? "guest",
            Password = _configuration["RabbitMq:Password"] ?? "guest"
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "cliente-criado",
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        var payload = JsonSerializer.Serialize(new
        {
            cliente.Id,
            cliente.Nome,
            cliente.Email
        });

        var body = Encoding.UTF8.GetBytes(payload);

        channel.BasicPublish(exchange: "",
                             routingKey: "cliente-criado",
                             basicProperties: null,
                             body: body);
    }
}
