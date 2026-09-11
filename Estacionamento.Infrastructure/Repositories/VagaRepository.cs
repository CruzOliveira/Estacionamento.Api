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
        private readonly IUnitOfWork _unitOfWork;

        public VagaRepository(EstacionamentoDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }
        public async Task CriacaoAsync(Vaga request)
        {
           var vagaExiste = await ObterPorIdAsync(request.Id);
            if (vagaExiste != null)
            {
                throw new IndexOutOfRangeException("Vaga já existe");
            }
            _context.Vagas.Add(request);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<Vaga?>> ListarAsync()
        {
          return await _context.Vagas.ToListAsync();
        }

        public async Task AtualizarAsync(Vaga vaga)
        {
            _context.Vagas.Update(vaga);
            await _unitOfWork.SaveChangesAsync();
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
                throw new KeyNotFoundException("Vaga não encontrada");
            }
            _context.Vagas.Remove(vagaExiste);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
