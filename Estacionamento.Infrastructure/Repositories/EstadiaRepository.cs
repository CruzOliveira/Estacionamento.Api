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
    public class EstadiaRepository : IEstadiaRepository
    {
        private readonly EstacionamentoDbContext _context;

        public EstadiaRepository(EstacionamentoDbContext context)
        {
            _context = context;
        }
        public async Task CriacaoAsync(Estadia request)
        {
            
            await _context.Estadias.AddAsync(request);
            await _context.SaveChangesAsync();
        }

        public async Task FinalizarAsync(Guid id)
        {
            var estadia = await ObterPorIdAsync(id);

            if (estadia == null)
            {
                throw new InvalidOperationException("Estadia não encontrada.");
            }

            _context.Estadias.Remove(estadia);
            await _context.SaveChangesAsync();


        }

        public async Task<IEnumerable<Estadia?>> ListarAsync()
        {
            return await _context.Estadias.ToListAsync();
        }

        public async Task<Estadia?> ObterPorIdAsync(Guid id)
        {
            return await _context.Estadias.FirstOrDefaultAsync(x => x.Id == id);
        } 
    }
}
