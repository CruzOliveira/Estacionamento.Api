using Estacionamento.Application.DTOs.Veiculo;
using Estacionamento.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Application.Services 
{
    public class VeiculoService : IVeiculoService
    {
        public Task<VeiculoResponse> CriacaoAsynk(CriarVeiculoRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<VeiculoResponse?> ObterPorIdAsynk(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<VeiculoResponse>> ListarAsynk()
        {
            throw new NotImplementedException();
        }

        public Task RemoverAsynk(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
