using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping
{
    public class CartoriosContatosMapping : IEntityTypeConfiguration<CartoriosContatos>
    {
        public void Configure(EntityTypeBuilder<CartoriosContatos> builder)
        {
            builder.ToTable("CartoriosContatos");

            builder.HasKey(e => e.IdCartorioContato);

            builder.Property(e => e.DataOperacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

			
            builder.HasOne(d => d.IdCartorioNavigation)
                        .WithMany(p => p.CartoriosContatos)
                        .HasForeignKey(d => d.IdCartorio)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("CartoriosContatos_Cartorios_FK");

            builder.HasOne(d => d.IdContatoNavigation)
                        .WithMany(p => p.CartoriosContatos)
                        .HasForeignKey(d => d.IdContato)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("CartoriosContatos_Contatos_FK");

            builder.HasOne(d => d.IdUsuarioNavigation)
                        .WithMany(p => p.CartoriosContatos)
                        .HasForeignKey(d => d.IdUsuario)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("CartoriosContatos_Usuarios_FK");
        }
    }
}
