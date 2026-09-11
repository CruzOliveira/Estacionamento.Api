using Estacionamento.Application.DTOs.Vaga;
using Estacionamento.Application.Interfaces;
using Estacionamento.Domain.Entities;
using Estacionamento.Domain.Enuns;
using Estacionamento.Domain.Interfaces;
using FluentValidation;
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
        private readonly IValidator<CriarVagaRequest> _validator;
        private readonly IUnitOfWork _unitOfWork;

        public VagaService(
            IVagaRepository vagaRepository,
            IValidator<CriarVagaRequest> validator,
            IUnitOfWork unitOfWork)
        {
            _vagaRepository = vagaRepository;
            _validator = validator;
            _unitOfWork = unitOfWork;
        }
        public async Task CriacaoAsync(CriarVagaRequest request)
        {
            await _validator.ValidateAndThrowAsync(request);

            var vaga = await ObterPorNumeroAsync(request.Numero);
            if (vaga != null)
            {
                throw new InvalidOperationException("Vaga já existe.");
            }
            
            Vaga novaVaga = new Vaga(
                request.Numero,
                request.Tipo
            );
            await _vagaRepository.CriacaoAsync(novaVaga);
            await _unitOfWork.SaveChangesAsync();
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
                Tipo = vaga.Tipo == TipoVeiculo.Moto ? "Moto" : "Carro",
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
            if (vaga == null) throw new KeyNotFoundException("Vaga não encontrada.");

            await _vagaRepository.RemoverAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
