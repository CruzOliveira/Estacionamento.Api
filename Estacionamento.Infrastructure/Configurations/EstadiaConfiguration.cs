using Estacionamento.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Infrastructure.Configurations
{
    public class EstadiaConfiguration : IEntityTypeConfiguration<Estadia>
    {
        public void Configure(EntityTypeBuilder<Estadia> builder)
        {
            builder.ToTable("Estadia");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Veiculo)
            .WithMany()
            .HasForeignKey(x => x.VeiculoId)
            .IsRequired();

            builder.HasOne(x => x.Vaga)
            .WithMany()
            .HasForeignKey(x => x.VagaId)
            .IsRequired();

            builder.Property(x => x.Entrada)
                .IsRequired();

            builder.Property(x => x.Saida)
                .IsRequired(false);

            builder.Property(x => x.Valor)
                .HasPrecision(10, 2)
                .IsRequired(false);
        }
    }
}


