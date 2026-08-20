using Estacionamento.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Infrastructure.Data
{
    public class EstacionamentoDbContext : DbContext
    {
        public EstacionamentoDbContext(DbContextOptions<EstacionamentoDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EstacionamentoDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Vaga> Vagas { get; set; }
        public DbSet<Estadia> Estadias { get; set; }


    }
}
