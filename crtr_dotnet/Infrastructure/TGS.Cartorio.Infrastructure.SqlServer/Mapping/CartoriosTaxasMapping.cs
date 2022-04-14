using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping
{
    public class CartoriosTaxasMapping : IEntityTypeConfiguration<CartoriosTaxas>
    {
        public void Configure(EntityTypeBuilder<CartoriosTaxas> builder)
        {
            builder.ToTable("CartoriosTaxas");

            builder.HasKey(e => e.IdCartorioTaxa)
                    .HasName("PK_CartorioTaxas");

            builder.HasOne(p => p.CartorioNavigation)
                .WithMany(p => p.CartoriosTaxas)
                .HasForeignKey(p => p.IdCartorio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CartoriosTaxasCartoriosFK");

            builder.HasOne(p => p.ProdutoModalidadeNavigation)
                .WithMany(p => p.CartoriosTaxas)
                .HasForeignKey(p => p.IdProdutoModalidade)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CartoriosTaxasProdutosModalidadesFK");


            //builder.HasOne(p => p.CartoriosServicosPcNavigation)
            //    .WithMany(p => p.CartoriosTaxas)
            //    .HasForeignKey(p => p.IdCartorioServico)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("");
        }
    }
}
