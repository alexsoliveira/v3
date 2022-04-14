using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping
{
    public class PerfisMapping : IEntityTypeConfiguration<Perfis>
    {
        public void Configure(EntityTypeBuilder<Perfis> builder)
        {
            builder.ToTable("Perfis");

            builder.HasKey(e => e.IdPerfil)
                   .HasName("PerfisPK");

            builder.Property(e => e.DataOperacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.FlagAtivo)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.Property(e => e.NomePerfil)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.HasOne(d => d.IdUsuarioOperacaoNavigation)
                .WithMany(p => p.Perfis)
                .HasForeignKey(d => d.IdUsuarioOperacao)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Perfil_Usuarios_FK");
        }
    }
}
