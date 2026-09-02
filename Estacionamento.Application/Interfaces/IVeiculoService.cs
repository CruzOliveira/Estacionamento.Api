using Estacionamento.Application.DTOs.Veiculo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Application.Interfaces
{
    public interface IVeiculoService
    {
        Task<VeiculoResponse> CriacaoAsync(CriarVeiculoRequest request);
        Task<VeiculoResponse?> ObterPorIdAsync(Guid id);
        Task<IEnumerable<VeiculoResponse>> ListarAsync();
        Task RemoverAsync(Guid id);

    }
}
