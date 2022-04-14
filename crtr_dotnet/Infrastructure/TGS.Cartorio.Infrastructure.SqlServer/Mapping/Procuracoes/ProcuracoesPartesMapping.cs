using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGS.Cartorio.Domain.Entities.Procuracoes;
using TGS.Cartorio.Infrastructure.SqlServer.Enumerable;

namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping.Procuracoes
{

    public class ProcuracoesPartesMapping : IEntityTypeConfiguration<ProcuracoesPartes>
    {
        public void Configure(EntityTypeBuilder<ProcuracoesPartes> builder)
        {
            builder.ToTable("ProcuracoesPartes", Schemas.Procuracoes.ToString());
            
            builder.HasKey(e => e.IdProcuracaoParte)
                    .HasName("Procuracoes_ProcuracoesPartesPK");

            builder.Property(e => e.IdProcuracaoParte)
                            .IsRequired();
            builder.Property(e => e.IdSolicitacao)
                            .IsRequired();
            builder.Property(e => e.IdPessoa)
                            .IsRequired();
            builder.Property(e => e.IdTipoProcuracaoParte)
                            .IsRequired();
            builder.Property(e => e.IdProcuracaoParteEstado)
                            .IsRequired();

            builder.Property(e => e.DataOperacao)
                            .HasColumnType("datetime")
                            .HasDefaultValueSql("(getdate())")
                            .IsRequired();

            builder.Property(e => e.Email)
                            .HasMaxLength(500);

            builder.Property(e => e.IdUsuario)
                            .IsRequired();

            builder.HasOne(d => d.PessoasNavigation)
                            .WithMany(p => p.ProcuracoesPartes)
                            .HasForeignKey(d => d.IdPessoa)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("Procuracoes_ProcuracoesPartesPessoasFK")
                            .IsRequired();

            builder.HasOne(d => d.ProcuracoesPartesEstadosPcNavigation)
                            .WithMany(p => p.ProcuracoesPartes)
                            .HasForeignKey(d => d.IdProcuracaoParteEstado)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("Procuracoes_ProcuracoesPartesProcuracoes_ProcuracoesPartesEstadosPCFK")
                            .IsRequired();

            builder.HasOne(d => d.SolicitacoesNavigation)
                            .WithMany(p => p.ProcuracoesPartes)
                            .HasForeignKey(d => d.IdSolicitacao)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("Procuracoes_ProcuracoesPartesSolicitacoesFK")
                            .IsRequired();

            builder.HasOne(d => d.TiposProcuracoesPartesNavigation)
                            .WithMany(p => p.ProcuracoesPartes)
                            .HasForeignKey(d => d.IdTipoProcuracaoParte)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("Procuracoes_ProcuracoesPartesProcuracoes_TiposProcuracoesPartesPCFK")
                            .IsRequired();

            builder.HasOne(d => d.UsuarioNavigation)
                            .WithMany(p => p.ProcuracoesPartes)
                            .HasForeignKey(d => d.IdUsuario)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("ProcuracoesProcuracoesPartesUsuariosFK")
                            .IsRequired();
        }
    }
}
