using Estacionamento.Application.DTOs.Veiculo;
using Estacionamento.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Estacionamento.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VeiculosController : ControllerBase
{
    private readonly IVeiculoService _veiculoService;

    public VeiculosController(IVeiculoService veiculoService)
    {
        _veiculoService = veiculoService;
    }

    [HttpPost]
    public async Task<ActionResult<VeiculoResponse>> CriarAsync([FromBody] CriarVeiculoRequest request)
    {
        var veiculo = await _veiculoService.CriacaoAsync(request);
        return StatusCode(StatusCodes.Status201Created, veiculo);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VeiculoResponse>>> ListarAsync()
    {
        return Ok(await _veiculoService.ListarAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<VeiculoResponse>> ObterPorIdAsync(Guid id)
    {
        var veiculo = await _veiculoService.ObterPorIdAsync(id);

        return veiculo is null
            ? NotFound(new { mensagem = "Veículo não encontrado." })
            : Ok(veiculo);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> RemoverAsync(Guid id)
    {
        await _veiculoService.RemoverAsync(id);
        return NoContent();
    }
}
