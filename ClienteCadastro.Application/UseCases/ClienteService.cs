using AutoMapper;
using ClienteCadastro.Application.DTOs;
using ClienteCadastro.Application.Interfaces;
using ClienteCadastro.Domain.Entities;
using ClienteCadastro.Domain.Interfaces;

namespace ClienteCadastro.Application.UseCases;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _repository;
    private readonly IViaCepService _viaCepService;
    private readonly IRabbitMqService _rabbitMqService;
    private readonly IMapper _mapper;

    public ClienteService(
        IClienteRepository repository,
        IViaCepService viaCepService,
        IRabbitMqService rabbitMqService,
        IMapper mapper)
    {
        _repository = repository;
        _viaCepService = viaCepService;
        _rabbitMqService = rabbitMqService;
        _mapper = mapper;
    }

    public async Task<ClienteDto> CriarClienteAsync(CreateClienteDto dto)
    {
        var clienteExistente = await _repository.ObterPorEmailAsync(dto.Email);
        if (clienteExistente != null)
            throw new Exception("Email já cadastrado.");

        var endereco = await _viaCepService.ObterEnderecoPorCepAsync(dto.Cep);

        var cliente = new Cliente(dto.Nome, dto.Email, dto.Cep, endereco);

        await _repository.AdicionarAsync(cliente);
        await _repository.SalvarAlteracoesAsync();

        // 🔔 Aqui está o envio da mensagem para o RabbitMQ
        _rabbitMqService.PublicarClienteCriado(cliente);

        return _mapper.Map<ClienteDto>(cliente);
    }

    public async Task<IEnumerable<ClienteDto>> ListarClientesAsync()
    {
        var clientes = await _repository.ObterTodosAsync(); // precisa existir no IClienteRepository
        return _mapper.Map<IEnumerable<ClienteDto>>(clientes);
    }

    public async Task<ClienteDto> ObterPorIdAsync(Guid id)
    {
        var cliente = await _repository.ObterPorIdAsync(id);
        if (cliente == null)
            throw new Exception("Cliente não encontrado.");

        return _mapper.Map<ClienteDto>(cliente);
    }

    public async Task<ClienteDto> AtualizarClienteAsync(Guid id, UpdateClienteDto dto)
    {
        var cliente = await _repository.ObterPorIdAsync(id);
        if (cliente == null)
            throw new Exception("Cliente não encontrado.");

        var endereco = await _viaCepService.ObterEnderecoPorCepAsync(dto.Cep);
        cliente.Atualizar(dto.Nome, dto.Cep, endereco);

        await _repository.SalvarAlteracoesAsync();

        return _mapper.Map<ClienteDto>(cliente);
    }

    public async Task<bool> RemoverClienteAsync(Guid id)
    {
        var cliente = await _repository.ObterPorIdAsync(id);
        if (cliente == null)
            return false;

        _repository.Remover(cliente); // precisa existir no IClienteRepository
        await _repository.SalvarAlteracoesAsync();
        return true;
    }
}
