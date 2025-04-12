using ClienteCadastro.Domain.Entities;

namespace ClienteCadastro.Domain.Interfaces;

public interface IRabbitMqService
{
    void PublicarClienteCriado(Cliente cliente);
}
