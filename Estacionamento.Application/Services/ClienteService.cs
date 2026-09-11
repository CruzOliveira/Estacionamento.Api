using Estacionamento.Application.DTOs.Cliente;
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
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IValidator<CriarClienteRequest> _validator;
        
        private readonly IUnitOfWork _unitOfWork;

        public ClienteService(
            IClienteRepository cliente,
            IValidator<CriarClienteRequest> validator,
            IUnitOfWork unitOfWork)
        {
            _clienteRepository = cliente;
            _validator = validator;
            _unitOfWork = unitOfWork;
        }

        public async  Task <ClienteResponse> AdicionarAsync(CriarClienteRequest request)
        {
            await _validator.ValidateAndThrowAsync(request);

            var clienteExiste = await _clienteRepository.ObterPorDocumentoAsynk(request.Documento);

            if (clienteExiste != null)
            {
                throw new InvalidOperationException("Cliente ja cadastrado");
            }
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var cliente = new Cliente(
                    request.Nome,
                    request.Documento
                    );
                await _clienteRepository.AdicionarAsync(cliente);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
                return new ClienteResponse
                {
                    Id = cliente.Id,
                    Nome = request.Nome,
                    Documento = cliente.Documento,
                };
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<ClienteResponse> ObterPorIdAsync(Guid id)
        {
            var cliente = await _clienteRepository.ObterPorIdAsync(id);
            if( cliente is null )
            {
                throw new KeyNotFoundException("Cliente não encontrado.");
            }
            return new ClienteResponse
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Documento = cliente.Documento
            };
        }

        public async Task<IEnumerable<ClienteResponse?>> ListarAsync()
        {
            var clientes = await _clienteRepository.ListarAsync();

            List<ClienteResponse>  listaCliente = new();

            foreach (var cliente in clientes)
            {
                listaCliente.Add(new ClienteResponse
                {
                    Id = cliente.Id,
                    Nome = cliente.Nome,
                    Documento = cliente.Documento
                });
            }
            return listaCliente;
        }

        public async Task RemoverAsync(Guid id)
        {

            await _unitOfWork.BeginTransactionAsync();
            try {
                await _clienteRepository.RemoverAsync(id);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
