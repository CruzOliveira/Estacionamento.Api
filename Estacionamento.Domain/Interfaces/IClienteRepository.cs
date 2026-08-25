using Estacionamento.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Domain.Interfaces
{
    public interface IClienteRepository
    {
        Task<Cliente?> ObterPorIdAsync(Guid id);
        Task<Cliente?> ObterPorDocumentoAsynk(string documento);
        Task<IEnumerable<Cliente?>> ListarAsynk();
        Task AdicionarAsync(Cliente cliente);
        Task RemoverAsync(Guid id);
    }
}
