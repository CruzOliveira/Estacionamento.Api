using Estacionamento.Application.DTOs.Vaga;
using Estacionamento.Application.Interfaces;
using Estacionamento.Domain.Entities;
using Estacionamento.Domain.Enuns;
using Estacionamento.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Application.Services
{
    public class VagaService : IVagaService
    {
        private readonly IVagaRepository _vagaRepository;

        public VagaService(IVagaRepository vagaRepository)
        {
            _vagaRepository = vagaRepository;
        }
        public async Task CriacaoAsync(CriarVagaRequest request)
        {
           
            var vaga = await ObterPorNumeroAsync(request.Numero);
            if (vaga != null)
            {
                throw new Exception("Vaga já existe.");
            }
            
            Vaga novaVaga = new Vaga(
                request.Numero,
                request.Tipo
            );
            await _vagaRepository.CriacaoAsync(novaVaga);

        }

        public async Task<IEnumerable<VagaResponse?>> ListarAsync()
        {
             var vagas = await _vagaRepository.ListarAsync();
             return vagas.Select(v => new VagaResponse
             {
                 Id = v.Id,
                 Numero = v.Numero,
                 Tipo = v.Tipo == TipoVeiculo.Moto ? "Moto" : "Carro",
                 Ocupada = v.Status == StatusVaga.Ocupada,
             });
        }

        public async Task<VagaResponse?> ObterPorNumeroAsync(int numero)
        {
            var vaga = await _vagaRepository.ObterPorNumeroAsync(numero);
            if (vaga == null) return null;

            return new VagaResponse
            {
                Id = vaga.Id,
                Numero = vaga.Numero,
                Ocupada = vaga.Status == StatusVaga.Ocupada,
            };
        }

        public async Task<IEnumerable<VagaResponse?>> ObterVagaDisponivelAsync(string tipo)
        {
            return (await ListarAsync()).Where(v => v != null && !v.Ocupada && v.Tipo == tipo);
        }

        public async Task RemoverAsync(Guid id)
        {
            var vaga = await _vagaRepository.ObterPorIdAsync(id);
            if (vaga == null) throw new Exception("Vaga não encontrada.");

            await _vagaRepository.RemoverAsync(id);
        }
    }
}
