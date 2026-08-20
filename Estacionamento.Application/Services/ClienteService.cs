using Estacionamento.Application.DTOs.Cliente;
using Estacionamento.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Application.Services
{
    public class ClienteService : IClienteService
    {
        public Task<ClienteResponse> CriacaoAsynk(CriarClienteRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ClienteResponse>> ListarAsynk()
        {
            throw new NotImplementedException();
        }

        public Task<ClienteResponse?> ObterPorIdAsynk(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task RemoverAsynk(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
