using Estacionamento.Application.DTOs.Veiculo;
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
    public class VeiculoService : IVeiculoService
    {
        private readonly IVeiculoRepository _veiculoRepository;
        private readonly IValidator<CriarVeiculoRequest> _validator;
        private readonly IUnitOfWork _unitOfWork;

        public VeiculoService(
            IVeiculoRepository veiculoRepository,
            IValidator<CriarVeiculoRequest> validator,
            IUnitOfWork unitOfWork)
        {
            _veiculoRepository = veiculoRepository;
            _validator = validator;
            _unitOfWork = unitOfWork;
        }

        public async Task<VeiculoResponse> CriacaoAsync(CriarVeiculoRequest request)
        {
            await _validator.ValidateAndThrowAsync(request);

            var veiculoExistente = await _veiculoRepository.ObterPorPlacaAsync(request.Placa);

            if (veiculoExistente != null)
            {
                throw new InvalidOperationException("Veículo com esta placa já está cadastrado.");
            }
            
            Veiculo veiculo = new Veiculo(request.Placa, request.Tipo, request.ClienteId);

            await _veiculoRepository.CriacaoAsync(veiculo);
            await _unitOfWork.SaveChangesAsync();

            return new VeiculoResponse
            {
                Id = veiculo.Id,
                Placa = veiculo.Placa,
                Tipo = veiculo.Tipo,
            };
        }

        public async Task<VeiculoResponse?> ObterPorIdAsync(Guid id)
        {
            var veiculo = await _veiculoRepository.ObterPorIdAsync(id);

            if (veiculo == null)
                return null;

            return new VeiculoResponse
            {
                Id = veiculo.Id,
                Placa = veiculo.Placa,
                Tipo = veiculo.Tipo,
            };


        }
        public async Task<IEnumerable<VeiculoResponse>> ListarAsync()
        {
            var veiculos = await _veiculoRepository.ListarAsync();

            var veiculosResponse = veiculos.Select(v => new VeiculoResponse
            {
                Id = v.Id,
                Placa = v.Placa,
                Tipo = v.Tipo,
            });
            return veiculosResponse;
        }

        public async Task RemoverAsync(Guid id)
        {
            var veiculoExistente = await _veiculoRepository.ObterPorIdAsync(id);

            if (veiculoExistente == null)
            {
                throw new KeyNotFoundException("Veículo não encontrado.");
            }

            await _veiculoRepository.RemoverAsync(veiculoExistente.Id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
