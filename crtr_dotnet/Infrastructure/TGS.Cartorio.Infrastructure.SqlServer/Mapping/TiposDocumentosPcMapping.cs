using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping
{
    public class TiposDocumentosPcMapping : IEntityTypeConfiguration<TiposDocumentosPc>
    {
        public void Configure(EntityTypeBuilder<TiposDocumentosPc> builder)
        {
            builder.ToTable("TiposDocumentosPC");

            builder.HasKey(e => e.IdTipoDocumento)
                    .HasName("TiposDocumentosPCPK");

            builder.Property(e => e.IdTipoDocumento).ValueGeneratedNever();
            builder.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);
            builder.Property(e => e.Observacao)
                .HasMaxLength(200)
                .IsUnicode(false);
        }
    }
}
