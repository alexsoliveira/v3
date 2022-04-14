using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGS.Cartorio.Domain.Entities.Procuracoes;
using TGS.Cartorio.Infrastructure.SqlServer.Enumerable;

namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping.Procuracoes
{

    public class MatrimoniosDocumentosMapping : IEntityTypeConfiguration<MatrimoniosDocumentos>
    {
        public void Configure(EntityTypeBuilder<MatrimoniosDocumentos> builder)
        {
            builder.ToTable("MatrimoniosDocumentos", Schemas.Procuracoes.ToString());

            builder.HasKey(e => e.IdMatrimonioDocumento)
                    .HasName("Procuracoes_MatrimoniosDocumentosPK");

            builder.Property(e => e.IdMatrimonioDocumento)
                            .IsRequired();
            builder.Property(e => e.IdMatrimonio)
                            .IsRequired();
            builder.Property(e => e.IdTipoDocumento)
                            .IsRequired();


            builder.Property(e => e.DataOperacao)
                            .HasColumnType("datetime")
                            .HasDefaultValueSql("(getdate())")
                            .IsRequired();

            builder.Property(e => e.IdUsuario)
                            .IsRequired();

            builder.HasOne(d => d.IdMatrimonioNavigation)
                            .WithMany(p => p.MatrimoniosDocumentos)
                            .HasForeignKey(d => d.IdMatrimonio)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("Procuracoes_MatrimoniosDocumentosProcuracoes_MatrimoniosFK");

            builder.HasOne(d => d.IdTipoDocumentoNavigation)
                            .WithMany(p => p.MatrimoniosDocumentos)
                            .HasForeignKey(d => d.IdTipoDocumento)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("Procuracoes_MatrimoniosDocumentosProcuracoes_MatrimonioTiposDocumentosPCFK");

            builder.HasOne(d => d.IdProcuracaoParteNavigation)
                            .WithMany(p => p.MatrimoniosDocumentos)
                            .HasForeignKey(d => d.IdProcuracaoParte)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("Procuracoes_MatrimoniosDocumentosProcuracoes_ProcuracoesPartesFK");

        }
    }
}
