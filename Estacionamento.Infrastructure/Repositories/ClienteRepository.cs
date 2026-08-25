using Estacionamento.Domain.Entities;
using Estacionamento.Domain.Interfaces;
using Estacionamento.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly EstacionamentoDbContext _context;

        public ClienteRepository(EstacionamentoDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Cliente veiculo)
        {
            await _context.Clientes.AddAsync(veiculo);
            await _context.SaveChangesAsync();
        }

        public async Task<Cliente?> ObterPorDocumentoAsynk(string documento)
        {
            return await _context.Clientes.FirstOrDefaultAsync(x => x.Documento == documento);
        }

        public Task<Cliente?> ObterPorIdAsync(Guid id)
        {
            return _context.Clientes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Cliente?>> ListarAsynk()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task RemoverAsync(Guid id)
        {
            var cliente = await ObterPorIdAsync(id);

            if (cliente == null)
            {
                return;
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
        }
    }
}
