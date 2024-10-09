using Confeitaria.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Confeitaria.Data.Mappings
{
    public class ClienteMapping : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(c => c.Cpf)
                .IsRequired()
                .HasColumnType("varchar(14)");

            builder.Property(c => c.Email)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(c => c.Telefone)
             .IsRequired()
             .HasColumnType("varchar(20)");

            // Relacionamento 1 pra N 
            builder.HasMany(c => c.Pedidos)
                .WithOne(p => p.Cliente);

            builder.ToTable("TB_CLIENTES");

        }
    }
}
