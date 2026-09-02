using Estacionamento.Application.DTOs.Vaga;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Application.Interfaces
{
    public interface IVagaService
    {
        Task CriacaoAsync(CriarVagaRequest request);
        Task<VagaResponse?> ObterPorNumeroAsync(int numero);
        Task<IEnumerable<VagaResponse?>> ListarAsync();
        Task<IEnumerable<VagaResponse?>> ObterVagaDisponivelAsync(string tipo);
        Task RemoverAsync(Guid id);
    }
}
