using Confeitaria.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Confeitaria.Data.Mappings
{
    public class PedidoMapping : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.QuantProduto)
                .IsRequired();

            // relacionamento
            builder.HasOne(p => p.Cliente)
               .WithMany(c => c.Pedidos); 

            builder.HasOne(p => p.Endereco)
                .WithOne(e => e.Pedido);

            builder.HasMany(p => p.Produtos)
                .WithMany(p => p.Pedidos)
                .UsingEntity(j => j.ToTable("PedidosProdutos"));

            builder.ToTable("TB_PEDIDOS");
        }
    }
}
