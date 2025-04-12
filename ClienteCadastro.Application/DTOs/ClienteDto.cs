namespace ClienteCadastro.Application.DTOs;

public class ClienteDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public string Cep { get; set; }
    public string Logradouro { get; set; }
    public string Bairro { get; set; }
    public string Localidade { get; set; }
    public string Cidade { get; set; }
    public string Uf { get; set; }
}
