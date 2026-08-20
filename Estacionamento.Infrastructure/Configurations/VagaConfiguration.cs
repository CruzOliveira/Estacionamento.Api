using Estacionamento.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamento.Infrastructure.Configurations
{
    public class VagaConfiguration : IEntityTypeConfiguration<Vaga>
    {
        public void Configure(EntityTypeBuilder<Vaga> builder)
        {
            builder.ToTable("Vaga");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Numero)
                .IsRequired();

            builder.HasIndex(x => x.Numero)
                .IsUnique();

            builder.Property(x => x.Tipo)
                .IsRequired();
            builder.Property(x => x.Status)
                .IsRequired();
        }
    }
}
