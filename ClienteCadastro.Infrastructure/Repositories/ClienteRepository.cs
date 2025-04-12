using ClienteCadastro.Domain.Entities;
using ClienteCadastro.Domain.Interfaces;
using ClienteCadastro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClienteCadastro.Infrastructure.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly AppDbContext _context;

    public ClienteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Cliente> ObterPorEmailAsync(string email)
    {
        return await _context.Clientes
            .FirstOrDefaultAsync(c => c.Email.ToLower() == email.ToLower());
    }

    public async Task<Cliente> ObterPorIdAsync(Guid id)
    {
        return await _context.Clientes.FindAsync(id);
    }

    public async Task<IEnumerable<Cliente>> ObterTodosAsync()
    {
        return await _context.Clientes.ToListAsync();
    }

    public async Task AdicionarAsync(Cliente cliente)
    {
        await _context.Clientes.AddAsync(cliente);
    }

    public void Remover(Cliente cliente)
    {
        _context.Clientes.Remove(cliente);
    }

    public async Task SalvarAlteracoesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
