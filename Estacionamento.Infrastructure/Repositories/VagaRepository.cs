using Azure.Core;
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
    public class VagaRepository : IVagaRepository
    {
        private readonly EstacionamentoDbContext _context;

        public VagaRepository(EstacionamentoDbContext context)
        {
            _context = context;
        }
        public async Task CriacaoAsync(Vaga request)
        {
           var vagaExiste = await ObterPorIdAsync(request.Id);
            if (vagaExiste != null)
            {
                throw new Exception("Vaga já existe");
            }
            _context.Vagas.Add(request);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Vaga?>> ListarAsync()
        {
          return await _context.Vagas.ToListAsync();
        }

        public async Task<Vaga?> ObterPorNumeroAsync(int numero)
        {
            return await _context.Vagas.FirstOrDefaultAsync(x => x.Numero == numero);
        }
        public async Task<Vaga?> ObterPorIdAsync(Guid id)
        {
            return await _context.Vagas.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task RemoverAsync(Guid id)
        {
            var vagaExiste = await ObterPorIdAsync(id);
            if (vagaExiste == null)
            {
                throw new Exception("Vaga não encontrada");
            }
            _context.Vagas.Remove(vagaExiste);
            await _context.SaveChangesAsync();
        }
    }
}
