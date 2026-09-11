using Estacionamento.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EstacionamentoDbContext _context;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(EstacionamentoDbContext context)
        {
            _context = context;
        }
        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction is null)
            {
                throw new InvalidOperationException("Nenhuma transação foi iniciada.");
            }
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction is null)
            {
                return;
            }
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

}
