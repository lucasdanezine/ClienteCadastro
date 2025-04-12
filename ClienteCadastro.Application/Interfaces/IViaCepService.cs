using ClienteCadastro.Domain.Entities;

namespace ClienteCadastro.Application.Interfaces;

public interface IViaCepService
{
    Task<Endereco> ObterEnderecoPorCepAsync(string cep);
}
