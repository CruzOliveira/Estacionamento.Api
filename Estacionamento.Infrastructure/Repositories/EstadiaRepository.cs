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
        private readonly IUnitOfWork _unitOfWork;

        public EstadiaRepository(EstacionamentoDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }
        public async Task CriacaoAsync(Estadia request)
        {
            
            await _context.Estadias.AddAsync(request);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Estadia estadia)
        {
            _context.Estadias.Update(estadia);
            await _unitOfWork.SaveChangesAsync();
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
