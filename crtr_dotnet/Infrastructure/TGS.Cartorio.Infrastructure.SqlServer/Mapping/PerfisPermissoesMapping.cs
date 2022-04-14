using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping
{
    public class PerfisPermissoesMapping : IEntityTypeConfiguration<PerfisPermissoes>
    {
        public void Configure(EntityTypeBuilder<PerfisPermissoes> builder)
        {
            builder.ToTable("PerfisPermissoes");

            builder.HasKey(e => e.IdPerfilPermissao)
                   .HasName("PerfilPermissoesPK");

            builder.Property(e => e.DataOperacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Permissoes)
                .IsRequired()
                .HasMaxLength(2000);

            builder.HasOne(d => d.IdPerfilNavigation)
                .WithMany(p => p.PerfisPermissoes)
                .HasForeignKey(d => d.IdPerfil)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PerfilPermissoes_Perfis_FK");

            builder.HasOne(d => d.IdUsuarioNavigation)
                .WithMany(p => p.PerfisPermissoes)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PerfilPermissoes_Usuarios_FK");
        }
    }
}
