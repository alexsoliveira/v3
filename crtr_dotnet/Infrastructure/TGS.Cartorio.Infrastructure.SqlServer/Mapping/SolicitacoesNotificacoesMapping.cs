using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping
{
    public class SolicitacoesNotificacoesMapping : IEntityTypeConfiguration<SolicitacoesNotificacoes>
    {
        public void Configure(EntityTypeBuilder<SolicitacoesNotificacoes> builder)
        {
            builder.ToTable("SolicitacoesNotificacoes");

            builder.HasKey(e => e.IdSolicitacaoNotificacao)
                      .HasName("SolicitacoesNotificacoesPK");

            builder.Property(e => e.Conteudo)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(e => e.DataOperacao).HasColumnType("datetime");

            builder.Property(e => e.Titulo)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.HasOne(d => d.IdSolicitacaoNavigation)
                .WithMany(p => p.SolicitacoesNotificacoes)
                .HasForeignKey(d => d.IdSolicitacao)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SolicitacoesNotificacoes_Solicitacoes_FK");

            builder.HasOne(d => d.IdSolicitacaoNotificacaoEstadoNavigation)
                .WithMany(p => p.SolicitacoesNotificacoes)
                .HasForeignKey(d => d.IdSolicitacaoNotificacaoEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SolicitacoesNotificacoes_SolicitacoesNotificacoesEstadosPC_FK");

            builder.HasOne(d => d.IdUsuarioNavigation)
                .WithMany(p => p.SolicitacoesNotificacoes)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SolicitacoesNotificacoes_Usuarios_FK");
        }
    }
}
