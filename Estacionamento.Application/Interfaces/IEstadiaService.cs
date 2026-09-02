using Estacionamento.Application.DTOs.Estadia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Application.Interfaces
{
    public interface IEstadiaService
    {
        Task CriacaoAsync (CriarEstadiaRequest request);
        Task <EstadiaResponse?> ObterPorIdAsync(Guid id);
        Task<IEnumerable<EstadiaResponse?>> ListarAsync();
        Task FinalizarAsync(Guid id);
    }
}
