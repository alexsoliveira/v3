using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGS.Cartorio.Domain.Entities.Procuracoes;
using TGS.Cartorio.Infrastructure.SqlServer.Enumerable;

namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping.Procuracoes
{

    public class MatrimonioTiposDocumentosPcMapping : IEntityTypeConfiguration<MatrimonioTiposDocumentosPc>
    {
        public void Configure(EntityTypeBuilder<MatrimonioTiposDocumentosPc> builder)
        {
            builder.ToTable("MatrimonioTiposDocumentosPC", Schemas.Procuracoes.ToString());

            builder.HasKey(e => e.IdTipoDocumento)
                    .HasName("Procuracoes_MatrimonioTiposDocumentosPCPK");

            builder.Property(e => e.IdTipoDocumento)
                            .IsRequired();

            builder.Property(e => e.Descricao)
                            .HasMaxLength(100)
                            .IsRequired();


        }
    }
}
