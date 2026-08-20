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
        Task<VeiculoResponse> CriacaoAsynk(CriarVeiculoRequest request);
        Task<VeiculoResponse?> ObterPorIdAsynk(Guid id);
        Task<IEnumerable<VeiculoResponse>> ListarAsynk();
        Task RemoverAsynk(Guid id);

    }
}
