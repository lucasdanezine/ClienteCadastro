using ClienteCadastro.Domain.Entities;

namespace ClienteCadastro.Domain.Interfaces;

public interface IClienteRepository
{
    Task<Cliente> ObterPorEmailAsync(string email);
    Task<Cliente> ObterPorIdAsync(Guid id);
    Task<IEnumerable<Cliente>> ObterTodosAsync();
    Task AdicionarAsync(Cliente cliente);
    void Remover(Cliente cliente);
    Task SalvarAlteracoesAsync();
}
