using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping
{
    public class CartoriosEnderecosMapping : IEntityTypeConfiguration<CartoriosEnderecos>
    {
        public void Configure(EntityTypeBuilder<CartoriosEnderecos> builder)
        {
            builder.ToTable("CartoriosEnderecos");

            builder.HasKey(e => e.IdCartorioEndereco);

            builder.Property(e => e.DataOperacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(d => d.IdCartorioNavigation)
                .WithMany(p => p.CartoriosEnderecos)
                .HasForeignKey(d => d.IdCartorio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CartoriosEnderecos_Cartorios_FK");

            builder.HasOne(d => d.IdEnderecoNavigation)
                .WithMany(p => p.CartoriosEnderecos)
                .HasForeignKey(d => d.IdEndereco)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CartoriosEnderecos_Enderecos_FK");

            builder.HasOne(d => d.IdUsuarioNavigation)
                .WithMany(p => p.CartoriosEnderecos)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CartoriosEnderecos_Usuarios_FK");
        }
    }
}
