using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping
{
    public class ProdutosModalidadesMapping : IEntityTypeConfiguration<ProdutosModalidades>
    {
        public void Configure(EntityTypeBuilder<ProdutosModalidades> builder)
        {
            builder.ToTable("ProdutosModalidades");

            builder.HasKey(e => e.IdProdutoModalidade)
                    .HasName("ProdutosModalidadesPK");

            builder.Property(e => e.DataOperacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(d => d.IdModalidadeNavigation)
                .WithMany(p => p.ProdutosModalidades)
                .HasForeignKey(d => d.IdModalidade)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ProdutosModalidades_ProdutosModalidadesPC_FK");

            builder.HasOne(d => d.IdProdutoNavigation)
                .WithMany(p => p.ProdutosModalidades)
                .HasForeignKey(d => d.IdProduto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ProdutosModalidades_Produtos_FK");

            builder.HasOne(d => d.IdUsuarioNavigation)
                .WithMany(p => p.ProdutosModalidades)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ProdutosModalidades_Usuarios_FK");
        }

    }
}
