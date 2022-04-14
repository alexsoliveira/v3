using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping
{
    public class UsuariosPerfisMapping : IEntityTypeConfiguration<UsuariosPerfis>
    {

        public void Configure(EntityTypeBuilder<UsuariosPerfis> builder)
        {
            builder.ToTable("UsuariosPerfis");

            builder.HasKey(e => e.IdUsuarioPerfil)
                   .HasName("IdUsuarioPerfilPK");

            builder.Property(e => e.DataOperacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.HasOne(d => d.IdPerfilNavigation)
                .WithMany(p => p.UsuariosPerfis)
                .HasForeignKey(d => d.IdPerfil)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UsuariosPerfis_Perfis_FK1");

            builder.HasOne(d => d.IdUsuarioNavigation)
                .WithMany(p => p.UsuariosPerfisIdUsuarioNavigation)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UsuariosPerfis_Usuarios_FK1");

            builder.HasOne(d => d.IdUsuarioOperacaoNavigation)
                .WithMany(p => p.UsuariosPerfisIdUsuarioOperacaoNavigation)
                .HasForeignKey(d => d.IdUsuarioOperacao)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UsuariosPerfis_Usuarios_FK");

        }
    }
}
