using Confeitaria.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Confeitaria.Data.Mappings
{
    public class PedidoProdutoMapping : IEntityTypeConfiguration<PedidoProduto>
    {
        public void Configure(EntityTypeBuilder<PedidoProduto> builder)
        {
            // chave primaria composta
            builder.HasKey(pp => new {pp.PedidoId , pp.ProdutoId});

            builder.Property(pp => pp.Quantidade)
            .IsRequired();

            // relacionamento com pedido
            builder.HasOne(p => p.Pedido)
            .WithMany(p => p.PedidoProdutos)
            .HasForeignKey(p => p.PedidoId);

            // relacionamento com produto
            builder.HasOne(p => p.Produto)
            .WithMany(p => p.PedidoProdutos)
            .HasForeignKey(p => p.ProdutoId);

            builder.ToTable("TB_PEDIDOSPRODUTOS");

        }
    }
}
