using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping
{
    public class ProdutosMapping : IEntityTypeConfiguration<Produtos>
    {
        public void Configure(EntityTypeBuilder<Produtos> builder)
        {
            builder.ToTable("Produtos");

            builder.HasKey(e => e.IdProduto)
                   .HasName("ProdutosPK");

            builder.Property(e => e.Campos).HasMaxLength(2000);

            builder.Property(e => e.DataOperacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(2000)
                .IsUnicode(false);

            builder.Property(e => e.FlagAtivo)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.SubTitulo)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.Titulo)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.HasOne(d => d.IdUsuarioNavigation)
                .WithMany(p => p.Produtos)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Produtos_Usuarios_FK");

            builder.HasOne(d => d.IdProdutoCategoriaNavigation)
                .WithMany(p => p.Produtos)
                .HasForeignKey(d => d.IdProdutoCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ProdutosProdutosCategoriasPCFK");
        }
    }
}
