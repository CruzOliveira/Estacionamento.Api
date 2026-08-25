using Estacionamento.Application.DTOs.Cliente;
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
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository cliente)
        {
            _clienteRepository = cliente;
        }

        public async  Task <ClienteResponse> AdicionarAsync(CriarClienteRequest request)
        {
            var clienteExiste = await _clienteRepository.ObterPorDocumentoAsynk(request.Documento);

            if (clienteExiste != null)
            {
                throw new Exception("Cliente ja cadastrado");
            }

            var cliente = new Cliente(
                request.Nome,
                request.Documento
                );

            await _clienteRepository.AdicionarAsync(cliente);

            return new ClienteResponse
            {
                Id= cliente.Id,
                Nome= request.Nome,
                Documento= cliente.Documento,
            };
        }

        public async Task<ClienteResponse> ObterPorIdAsync(Guid id)
        {
            var cliente = await _clienteRepository.ObterPorIdAsync(id);


            return new ClienteResponse
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                Documento = cliente.Documento
            };
        }

        public async Task<IEnumerable<ClienteResponse?>> ListarAsynk()
        {
            var clientes = await _clienteRepository.ListarAsynk();

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
            await _clienteRepository.RemoverAsync(id);    
        }
    }
}
