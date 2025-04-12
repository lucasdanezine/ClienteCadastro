namespace ClienteCadastro.Domain.Entities;

public class Cliente
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public string Cep { get; private set; }
    public Endereco Endereco { get; private set; }

    // Construtor protegido pro EF
    protected Cliente() { }

    public Cliente(string nome, string email, string cep, Endereco endereco)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Email = email;
        Cep = cep;
        Endereco = endereco ?? throw new ArgumentNullException(nameof(endereco));
    }

    public void Atualizar(string nome, string cep, Endereco endereco)
    {
        Nome = nome;
        Cep = cep;
        Endereco = endereco ?? throw new ArgumentNullException(nameof(endereco));
    }
}
