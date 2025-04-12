using ClienteCadastro.Application.DTOs;
using ClienteCadastro.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClienteCadastro.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        // POST: api/clientes
        [HttpPost]
        public async Task<IActionResult> CriarCliente([FromBody] CreateClienteDto dto)
        {
            try
            {
                var cliente = await _clienteService.CriarClienteAsync(dto);
                return CreatedAtAction(nameof(ObterPorId), new { id = cliente.Id }, cliente);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/clientes
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var clientes = await _clienteService.ListarClientesAsync();
            return Ok(clientes);
        }

        // GET: api/clientes/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            try
            {
                var cliente = await _clienteService.ObterPorIdAsync(id);
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // PUT: api/clientes/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] UpdateClienteDto dto)
        {
            try
            {
                var cliente = await _clienteService.AtualizarClienteAsync(id, dto);
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/clientes/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Remover(Guid id)
        {
            var sucesso = await _clienteService.RemoverClienteAsync(id);
            if (!sucesso)
                return NotFound(new { message = "Cliente não encontrado." });

            return NoContent();
        }
    }
}
