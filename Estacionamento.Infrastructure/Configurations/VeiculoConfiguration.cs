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
    public class VeiculoConfiguration : IEntityTypeConfiguration<Veiculo>
    {
        public void Configure(EntityTypeBuilder<Veiculo> builder)
        {
            builder.ToTable("Veiculo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Placa)
                .IsRequired()
                .HasMaxLength(10);

            builder.HasIndex(x => x.Placa)
                .IsUnique();

            builder.Property(x => x.Placa)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Tipo)
                .IsRequired();

            builder.HasOne(x => x.Cliente)
                .WithMany(x => x.Veiculos)
                .HasForeignKey(x => x.ClienteId)
                .IsRequired();

        }
    }
}
