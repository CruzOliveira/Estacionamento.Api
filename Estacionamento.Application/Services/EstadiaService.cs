using Estacionamento.Application.DTOs.Estadia;
using Estacionamento.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Application.Services
{
    public class EstadiaService : IEstadiaService
    {
        private readonly IClienteService _clienteService;
        private readonly IVeiculoService _veiculoService;  
        private readonly IVagaService _vagaService;

        public EstadiaService(IClienteService clienteService, IVeiculoService veiculoService, IVagaService vagaService)
        {
            _clienteService = clienteService;
            _veiculoService = veiculoService;
            _vagaService = vagaService;
        }

        public async Task CriacaoAsync(CriarEstadiaRequest request)
        {
           var veiculo = _veiculoService.ObterPorIdAsync(request.VeiculoId);
            if (veiculo == null)
            {
                throw new Exception("Veículo não encontrado.");
            }
           var vagas = _vagaService.ObterVagaDisponivelAsync(request.TipoVeiculo);
            
            foreach (var vaga in vagas)
            {
                
            }
            throw new NotImplementedException();
        }

        public Task FinalizarAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EstadiaResponse?>> ListarAsync()
        {
            throw new NotImplementedException();
        }

        public Task<EstadiaResponse?> ObterPorIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
