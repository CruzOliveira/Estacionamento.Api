using Estacionamento.Application.DTOs.Estadia;
using Estacionamento.Application.Interfaces;
using Estacionamento.Domain.Entities;
using Estacionamento.Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Application.Services
{
    public class EstadiaService : IEstadiaService
    {
        private const decimal ValorHora = 10m;
        private readonly IVeiculoRepository _veiculoRepository;
        private readonly IVagaRepository _vagaRepository;
        private readonly IEstadiaRepository _estadiaRepository;
        private readonly IValidator<CriarEstadiaRequest> _validator;
        private readonly IUnitOfWork _unitOfWork;

        public EstadiaService(
            IVeiculoRepository veiculoRepository,
            IVagaRepository vagaRepository,
            IEstadiaRepository estadiaRepository,
            IValidator<CriarEstadiaRequest> validator,
            IUnitOfWork unitOfWork)
        {
            _veiculoRepository = veiculoRepository;
            _vagaRepository = vagaRepository;
            _estadiaRepository = estadiaRepository;
            _validator = validator;
            _unitOfWork = unitOfWork;
        }

        public async Task CriacaoAsync(CriarEstadiaRequest request)
        {
            await _validator.ValidateAndThrowAsync(request);

            var veiculo = await _veiculoRepository.ObterPorIdAsync(request.VeiculoId);
            if (veiculo == null)
            {
                throw new Exception("Veículo não encontrado.");
            }

            var estadiasAtivas = await _estadiaRepository.ListarAsync();
            if (estadiasAtivas.Any(e => e?.VeiculoId == request.VeiculoId && !e.Saida.HasValue))
            {
                throw new InvalidOperationException("O veículo já possui uma estadia em aberto.");
            }

            var vagas = await _vagaRepository.ListarAsync();
            var vaga = vagas.FirstOrDefault(v => v != null &&
                v.Tipo == veiculo.Tipo &&
                v.Status == Domain.Enuns.StatusVaga.Disponivel);

            if (vaga == null)
            {
                throw new InvalidOperationException("Não há vaga disponível para este tipo de veículo.");
            }

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                vaga.Ocupar();
                var estadia = new Estadia(veiculo.Id, vaga.Id, DateTime.UtcNow);

                await _vagaRepository.AtualizarAsync(vaga);
                await _estadiaRepository.CriacaoAsync(estadia);


                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task FinalizarAsync(Guid id)
        {
            var estadia = await _estadiaRepository.ObterPorIdAsync(id)
                ?? throw new InvalidOperationException("Estadia não encontrada.");

            if (estadia.Saida.HasValue)
            {
                throw new InvalidOperationException("Esta estadia já foi finalizada.");
            }

            var vaga = await _vagaRepository.ObterPorIdAsync(estadia.VagaId)
                ?? throw new InvalidOperationException("Vaga da estadia não encontrada.");



            await _unitOfWork.BeginTransactionAsync();
            try
            {

                var saida = DateTime.UtcNow;
                var horasCobradas = Math.Max(1, (int)Math.Ceiling((saida - estadia.Entrada).TotalHours));
                estadia.Finalizar(saida, horasCobradas * ValorHora);
                vaga.Liberar();

                await _estadiaRepository.AtualizarAsync(estadia);
                await _vagaRepository.AtualizarAsync(vaga);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<IEnumerable<EstadiaResponse?>> ListarAsync()
        {
            var estadias = await _estadiaRepository.ListarAsync();
            return estadias
                .Where(e => e != null)
                .Select(e => MapearResposta(e!));
        }

        public async Task<EstadiaResponse?> ObterPorIdAsync(Guid id)
        {
            var estadia = await _estadiaRepository.ObterPorIdAsync(id);
            return estadia == null ? null : MapearResposta(estadia);
        }

        private static EstadiaResponse MapearResposta(Estadia estadia) => new()
        {
            Id = estadia.Id,
            VeiculoId = estadia.VeiculoId,
            VagaId = estadia.VagaId,
            Entrada = estadia.Entrada,
            Saida = estadia.Saida,
            Valor = estadia.Valor
        };
    }
}
