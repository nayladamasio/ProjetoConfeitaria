using Confeitaria.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Confeitaria.Data.Mappings
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(p => p.Descricao)
                .IsRequired()
                .HasColumnType("varchar(Max)");
          
            builder.Property(p => p.Imagem)
              .HasColumnType("varchar(100)");
          
            builder.Property(p => p.Peso)
              .IsRequired()
              .HasColumnType("varchar(50)");

            builder.ToTable("TB_PRODDUTOS");


        }
    }
}
