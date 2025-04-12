namespace ClienteCadastro.Infrastructure.Services.Models;

public class ViaCepResponse
{
    public string Cep { get; set; }
    public string Logradouro { get; set; }
    public string Bairro { get; set; }
    public string Localidade { get; set; } // cidade
    public string Uf { get; set; } // estado
    public bool Erro => string.IsNullOrEmpty(Logradouro) && string.IsNullOrEmpty(Localidade);
}
