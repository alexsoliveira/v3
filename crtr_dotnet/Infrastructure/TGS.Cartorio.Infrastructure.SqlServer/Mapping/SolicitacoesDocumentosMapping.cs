using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping
{
    public class SolicitacoesDocumentosMapping : IEntityTypeConfiguration<SolicitacoesDocumentos>
    {
        public void Configure(EntityTypeBuilder<SolicitacoesDocumentos> builder)
        {
            builder.ToTable("SolicitacoesDocumentos");

            builder.HasKey(e => e.IdSolicitacaoDocumento)
                   .HasName("SolicitacoesDocumentosPK");

            builder.Property(e => e.BlobConteudo).IsRequired();

            builder.Property(e => e.DataOperacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");


            builder.HasOne(d => d.IdTipoDocumentoNavigation)
                .WithMany(p => p.SolicitacoesDocumentos)
                .HasForeignKey(d => d.IdTipoDocumento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SolicitacoesDocumentos_TiposDocumentosPC_FK");

            builder.HasOne(d => d.IdUsuarioNavigation)
                .WithMany(p => p.SolicitacoesDocumentos)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SolicitacoesDocumentos_Usuarios_FK");
        }
    }
}
