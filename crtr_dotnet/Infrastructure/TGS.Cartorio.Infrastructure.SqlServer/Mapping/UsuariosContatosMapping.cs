using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping
{
    public class UsuariosContatosMapping : IEntityTypeConfiguration<UsuariosContatos>
    {
        public void Configure(EntityTypeBuilder<UsuariosContatos> builder)
        {
            builder.ToTable("UsuariosContatos");

            builder.HasKey(e => e.IdUsuarioContato)
                   .HasName("UsuariosContatosPK");

            builder.Property(e => e.DataOperacao).HasColumnType("datetime");
            builder.HasOne(d => d.IdContatoNavigation)
                .WithMany(p => p.UsuariosContatos)
                .HasForeignKey(d => d.IdContato)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UsuariosContatos_Contatos_FK");

            builder.HasOne(d => d.IdUsuarioNavigation)
                .WithMany(p => p.UsuariosContatos)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UsuariosContatos_Usuarios_FK");
        }
    }
}
