using Estacionamento.Application.DTOs.Vaga;
using Estacionamento.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Estacionamento.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VagasController : ControllerBase
{
    private readonly IVagaService _vagaService;

    public VagasController(IVagaService vagaService)
    {
        _vagaService = vagaService;
    }

    [HttpPost]
    public async Task<IActionResult> CriarAsync([FromBody] CriarVagaRequest request)
    {
        await _vagaService.CriacaoAsync(request);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VagaResponse?>>> ListarAsync()
    {
        return Ok(await _vagaService.ListarAsync());
    }

    [HttpGet("numero/{numero:int}")]
    public async Task<ActionResult<VagaResponse>> ObterPorNumeroAsync(int numero)
    {
        var vaga = await _vagaService.ObterPorNumeroAsync(numero);

        return vaga is null
            ? NotFound(new { mensagem = "Vaga não encontrada." })
            : Ok(vaga);
    }

    [HttpGet("disponiveis")]
    public async Task<ActionResult<IEnumerable<VagaResponse?>>> ListarDisponiveisAsync([FromQuery] string tipo)
    {
        return Ok(await _vagaService.ObterVagaDisponivelAsync(tipo));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> RemoverAsync(Guid id)
    {
        await _vagaService.RemoverAsync(id);
        return NoContent();
    }
}
