using Estacionamento.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Estacionamento.Infrastructure.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> bulder)
        {
            bulder.ToTable("Cliente");

            bulder.HasKey(x => x.Id);

            bulder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(100);

            bulder.Property(x => x.Documento)
                .IsRequired()
                .HasMaxLength(20);
        }

    }
}
