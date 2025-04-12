using ClienteCadastro.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClienteCadastro.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Cliente> Clientes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.ToTable("Clientes");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.HasIndex(e => e.Email).IsUnique();

            entity.Property(e => e.Cep).IsRequired().HasMaxLength(8);
            entity.OwnsOne(e => e.Endereco, endereco =>
            {
                endereco.Property(p => p.Logradouro).HasColumnName("Logradouro").HasMaxLength(100);
                endereco.Property(p => p.Bairro).HasColumnName("Bairro").HasMaxLength(100);
                endereco.Property(p => p.Cidade).HasColumnName("Cidade").HasMaxLength(100);
                endereco.Property(p => p.Estado).HasColumnName("Estado").HasMaxLength(2);
            });
        });

        base.OnModelCreating(modelBuilder);
    }
}
