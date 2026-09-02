using Estacionamento.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Domain.Interfaces
{
    public interface IVagaRepository
    {
        Task CriacaoAsync(Vaga request);
        Task<Vaga?> ObterPorIdAsync(Guid id);
        Task<Vaga?> ObterPorNumeroAsync(int numero);
        Task<IEnumerable<Vaga?>> ListarAsync();
        Task RemoverAsync(Guid id);
    }
}
