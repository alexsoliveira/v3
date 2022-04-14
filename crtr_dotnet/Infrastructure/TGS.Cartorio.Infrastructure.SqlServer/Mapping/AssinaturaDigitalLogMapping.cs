using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping
{
    public class AssinaturaDigitalLogMapping : IEntityTypeConfiguration<AssinaturaDigitalLog>
    {
        public void Configure(EntityTypeBuilder<AssinaturaDigitalLog> builder)
        {
            builder.ToTable("AssinaturaDigitalLog");

            builder.HasKey(e => e.IdAssinaturaDigitalLog).HasName("AssinaturaDigitalLogPK");

            builder.Property(e => e.DataOperacao)
                            .HasColumnType("datetime")
                            .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Observacao)
                            .IsRequired()
                            .HasMaxLength(2000)
                            .IsUnicode(false);

            builder.HasOne(d => d.IdSolicitacaoNavigation)
                            .WithMany(p => p.AssinaturaDigitalLog)
                            .HasForeignKey(d => d.IdSolicitacao)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("AssinaturaDigitalLog_Solicitacoes_FK");

            builder.HasOne(d => d.IdUsuarioNavigation)
                            .WithMany(p => p.AssinaturaDigitalLog)
                            .HasForeignKey(d => d.IdUsuario)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("AssinaturaDigitalLog_Usuarios_FK");

        }
    }
}
