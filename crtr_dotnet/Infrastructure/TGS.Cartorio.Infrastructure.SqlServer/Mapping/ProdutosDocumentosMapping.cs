using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping
{
    public class ProdutosDocumentosMapping : IEntityTypeConfiguration<ProdutosDocumentos>
    {
        public void Configure(EntityTypeBuilder<ProdutosDocumentos> builder)
        {
            builder.ToTable("ProdutosDocumentos");

            builder.HasKey(e => e.IdProdutoDocumentos)
                   .HasName("ProdutoDocumentosPK");

            builder.Property(e => e.DataOperacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.FlagAssinaturaDigital).HasDefaultValueSql("((0))");

            builder.HasOne(d => d.IdProdutoNavigation)
                .WithMany(p => p.ProdutosDocumentos)
                .HasForeignKey(d => d.IdProduto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ProdutosDocumentos_Produtos_FK");

            builder.HasOne(d => d.IdTipoDocumentoNavigation)
                .WithMany(p => p.ProdutosDocumentos)
                .HasForeignKey(d => d.IdTipoDocumento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ProdutosDocumentos_TiposDocumentosPC_FK");

            builder.HasOne(d => d.IdUsuarioNavigation)
                .WithMany(p => p.ProdutosDocumentos)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ProdutosDocumentos_Usuarios_FK");

        }
    }
}
