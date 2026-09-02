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
    public class VeiculoRepository : IVeiculoRepository
    {
        private readonly EstacionamentoDbContext _context;

        public VeiculoRepository(EstacionamentoDbContext context)
        {
            _context = context;
        }

        public async Task CriacaoAsync(Veiculo veiculo)
        {
            await _context.Veiculos.AddAsync(veiculo);
            await _context.SaveChangesAsync();
        }

        public async Task<Veiculo?> ObterPorIdAsync(Guid id)
        {
            return await _context.Veiculos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<Veiculo?> ObterPorPlacaAsync(string placa)
        {
            return _context.Veiculos.FirstOrDefaultAsync(x => x.Placa == placa);
        }

        public async Task<IEnumerable<Veiculo?>> ListarAsync()
        {
            return await _context.Veiculos.ToListAsync();
        }

        public async Task RemoverAsync(Guid id)
        {
            var veiculo = await ObterPorIdAsync(id);

            if (veiculo != null)
            {
                return;
            }    
            
            _context.Veiculos.Remove(veiculo);
            await _context.SaveChangesAsync();
        }
    }
}
