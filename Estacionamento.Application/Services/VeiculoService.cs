using Estacionamento.Application.DTOs.Veiculo;
using Estacionamento.Application.Interfaces;
using Estacionamento.Domain.Entities;
using Estacionamento.Domain.Interfaces;
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

        public VeiculoService(IVeiculoRepository veiculoRepository)
        {
            _veiculoRepository = veiculoRepository;
        }

        public async Task<VeiculoResponse> CriacaoAsync(CriarVeiculoRequest request)
        {
            

            var veiculoExistente = await _veiculoRepository.ObterPorPlacaAsync(request.Placa);

            if (veiculoExistente != null)
            {
                throw new InvalidOperationException("Veículo com esta placa já está cadastrado.");
            }
            
            Veiculo veiculo = new Veiculo(request.Placa, request.Tipo, request.ClienteId);

            await _veiculoRepository.CriacaoAsync(veiculo);

            return new VeiculoResponse
            {
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
                Placa = veiculo.Placa,
                Tipo = veiculo.Tipo,
            };


        }
        public async Task<IEnumerable<VeiculoResponse>> ListarAsync()
        {
            var veiculos = await _veiculoRepository.ListarAsync();

            var veiculosResponse = veiculos.Select(v => new VeiculoResponse
            {
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
                throw new InvalidOperationException("Veículo não encontrado.");
            }

            await _veiculoRepository.RemoverAsync(veiculoExistente.Id);
        }
    }
}
