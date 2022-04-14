using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping
{
    public class ProdutosImagensMapping : IEntityTypeConfiguration<ProdutosImagens>
    {
        public void Configure(EntityTypeBuilder<ProdutosImagens> builder)
        {
            builder.ToTable("ProdutosImagens");

            builder.HasKey(e => e.IdProdutoImagem)
                    .HasName("ProdutoImagensPK");

            builder.Property(e => e.BlobConteudo).IsRequired();

            builder.Property(e => e.DataOperacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(d => d.IdProdutoNavigation)
                .WithMany(p => p.ProdutosImagens)
                .HasForeignKey(d => d.IdProduto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ProdutosImagens_Produtos_FK");

            builder.HasOne(d => d.IdUsuarioNavigation)
                .WithMany(p => p.ProdutosImagens)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ProdutosImagens_Usuarios_FK");
        }
    }
}
