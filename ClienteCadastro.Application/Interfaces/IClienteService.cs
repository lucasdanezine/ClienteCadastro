using ClienteCadastro.Application.DTOs;

namespace ClienteCadastro.Application.Interfaces
{
    public interface IClienteService
    {
        Task<ClienteDto> CriarClienteAsync(CreateClienteDto dto);
        Task<IEnumerable<ClienteDto>> ListarClientesAsync();
        Task<ClienteDto> ObterPorIdAsync(Guid id);
        Task<ClienteDto> AtualizarClienteAsync(Guid id, UpdateClienteDto dto);
        Task<bool> RemoverClienteAsync(Guid id);
    }
}
