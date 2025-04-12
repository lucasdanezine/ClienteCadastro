using ClienteCadastro.Application.DTOs;
using ClienteCadastro.Application.Interfaces;

namespace ClienteCadastro.Application.UseCases;

public class CreateClienteHandler
{
    private readonly IClienteService _clienteService;

    public CreateClienteHandler(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    public async Task<ClienteDto> Handle(CreateClienteDto dto)
    {
        return await _clienteService.CriarClienteAsync(dto);
    }
}
