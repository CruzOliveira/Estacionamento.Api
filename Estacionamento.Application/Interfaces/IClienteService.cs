using Estacionamento.Application.DTOs.Cliente;
using Estacionamento.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Application.Interfaces
{
    public interface IClienteService
    {
        Task<ClienteResponse> ObterPorIdAsync(Guid id);
        Task<IEnumerable<ClienteResponse?>> ListarAsync();
        Task <ClienteResponse> AdicionarAsync(CriarClienteRequest cliente);
        Task RemoverAsync(Guid id);
    }
}
