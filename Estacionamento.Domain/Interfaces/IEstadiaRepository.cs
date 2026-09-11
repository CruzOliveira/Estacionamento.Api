using Estacionamento.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Domain.Interfaces
{
    public interface IEstadiaRepository
    {
        Task CriacaoAsync(Estadia request);
        Task<Estadia?> ObterPorIdAsync(Guid id);
        Task<IEnumerable<Estadia?>> ListarAsync();
        Task AtualizarAsync(Estadia estadia);
    }
}
