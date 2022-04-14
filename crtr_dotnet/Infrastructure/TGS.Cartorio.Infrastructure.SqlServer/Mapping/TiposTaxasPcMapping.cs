using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping
{
    public class TiposTaxasPcMapping : IEntityTypeConfiguration<TiposTaxasPc>
    {
        public void Configure(EntityTypeBuilder<TiposTaxasPc> builder)
        {
            builder.HasKey(e => e.IdTipoTaxa)
                   .HasName("TaxasPCPK");

            builder.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

        }
    }
}
