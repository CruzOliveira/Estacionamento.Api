using Estacionamento.Application.DTOs.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Application.Interfaces
{
    public interface IClienteService
    {
        Task<ClienteResponse> CriacaoAsynk(CriarClienteRequest request);
        Task<ClienteResponse?> ObterPorIdAsynk(Guid id);
        Task<IEnumerable<ClienteResponse>> ListarAsynk();
        Task RemoverAsynk(Guid id);
    }
}
