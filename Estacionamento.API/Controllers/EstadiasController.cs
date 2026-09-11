using Estacionamento.Application.DTOs.Estadia;
using Estacionamento.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Estacionamento.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstadiasController : ControllerBase
    {
        private readonly IEstadiaService _estadiaService;

        public EstadiasController(IEstadiaService estadiaService)
        {
            _estadiaService = estadiaService;
        }

        [HttpPost]
        public async Task<IActionResult> CriacaoAsync([FromBody] CriarEstadiaRequest request)
        {
            await _estadiaService.CriacaoAsync(request);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstadiaResponse?>>> ListarAsync()
        {
            var estadias = await _estadiaService.ListarAsync();
            return Ok(estadias);
        }
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<EstadiaResponse>> ObterPorIdAsync(Guid id)
        {
            var estadia = await _estadiaService.ObterPorIdAsync(id);
            if (estadia == null)
            {
                return NotFound(new { mensagem = "Estadia não encontrada." });
            }

            return Ok(estadia);
        }
        [HttpPatch("{id:guid}/finalizar")]
        public async Task<IActionResult> FinalizarAsync(Guid id)
        {
            await _estadiaService.FinalizarAsync(id);
            return NoContent();
        }
    }
}
