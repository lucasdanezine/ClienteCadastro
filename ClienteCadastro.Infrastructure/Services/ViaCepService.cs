using ClienteCadastro.Domain.Entities;
using ClienteCadastro.Application.Interfaces;
using Refit;

namespace ClienteCadastro.Infrastructure.Services;

public class ViaCepService : IViaCepService
{
    private readonly IViaCepApi _viaCepApi;

    public ViaCepService(IViaCepApi viaCepApi)
    {
        _viaCepApi = viaCepApi;
    }

    public async Task<Endereco> ObterEnderecoPorCepAsync(string cep)
    {
        var resposta = await _viaCepApi.BuscarEnderecoPorCepAsync(cep);

        if (resposta == null || resposta.Erro)
        {
            throw new Exception("CEP inválido.");
        }

        return new Endereco(
            logradouro: resposta.Logradouro,
            bairro: resposta.Bairro,
            cidade: resposta.Localidade,
            estado: resposta.Uf
        );
    }
}
