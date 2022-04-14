using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping
{
     public class ProdutosCategoriaPcMapping : IEntityTypeConfiguration<ProdutosCategoriasPc>
    {
        public void Configure(EntityTypeBuilder<ProdutosCategoriasPc> builder)
        {
            builder.ToTable("ProdutosCategoriasPC");

            builder.HasKey(p => p.IdProdutoCategoria)
                   .HasName("ProdutosCategoriasPCPK");

            builder.Property(e => e.IdProdutoCategoria)
                .IsRequired();

            builder.Property(e => e.Descricao)
                .HasColumnType("varchar(max)")
                .IsRequired();

            builder.Property(e => e.Titulo)
                .HasMaxLength(200);
        }
    }
}
