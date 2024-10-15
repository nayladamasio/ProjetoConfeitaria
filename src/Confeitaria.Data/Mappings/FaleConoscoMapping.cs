using Confeitaria.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Confeitaria.Data.Mappings
{
    public class FaleConoscoMapping : IEntityTypeConfiguration<FaleConosco>
    {
        public void Configure(EntityTypeBuilder<FaleConosco> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Nome)
                   .IsRequired()
                   .HasColumnType("varchar(100)");

            builder.Property(f => f.Email)
                   .IsRequired()
                   .HasColumnType("varchar(100)");

            builder.Property(f => f.Mensagem)
                   .IsRequired()
                   .HasColumnType("varchar(Max)");

            builder.ToTable("TB_FALECONOSCO");

        }
    }
}
