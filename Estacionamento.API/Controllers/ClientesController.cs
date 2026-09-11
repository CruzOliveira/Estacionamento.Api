using Estacionamento.Application.DTOs.Cliente;
using Estacionamento.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Estacionamento.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly IClienteService _clienteService;

    public ClientesController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    [HttpPost]
    public async Task<ActionResult<ClienteResponse>> CriarAsync([FromBody] CriarClienteRequest request)
    {
        var cliente = await _clienteService.AdicionarAsync(request);
        return CreatedAtRoute("ObterClientePorId", new { id = cliente.Id }, cliente);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClienteResponse?>>> ListarAsync()
    {
        return Ok(await _clienteService.ListarAsync());
    }

    [HttpGet("{id:guid}", Name = "ObterClientePorId")]
    public async Task<ActionResult<ClienteResponse>> ObterPorIdAsync(Guid id)
    {
        var cliente = await _clienteService.ObterPorIdAsync(id);
        return Ok(cliente);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> RemoverAsync(Guid id)
    {
        await _clienteService.RemoverAsync(id);
        return NoContent();
    }
}
