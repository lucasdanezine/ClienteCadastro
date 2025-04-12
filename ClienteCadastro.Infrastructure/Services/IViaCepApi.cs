using Refit;
using ClienteCadastro.Infrastructure.Services.Models;

namespace ClienteCadastro.Infrastructure.Services;

public interface IViaCepApi
{
    [Get("/ws/{cep}/json/")]
    Task<ViaCepResponse> BuscarEnderecoPorCepAsync(string cep);
}
