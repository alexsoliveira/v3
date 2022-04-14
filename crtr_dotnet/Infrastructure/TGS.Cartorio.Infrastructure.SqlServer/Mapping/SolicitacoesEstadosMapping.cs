using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping
{

    public class SolicitacoesEstadosMapping : IEntityTypeConfiguration<SolicitacoesEstados>
    {
        public void Configure(EntityTypeBuilder<SolicitacoesEstados> builder)
        {
            builder.ToTable("SolicitacoesEstados");

            builder.HasKey(e => e.IdSolicitacaoEstado)
                    .HasName("SolicitacoesEstadosPK");

            builder.Property(e => e.DataOperacao)
                            .HasColumnType("datetime")
                            .HasDefaultValueSql("(getdate())");

            builder.HasOne(d => d.IdEstadoNavigation)
                            .WithMany(p => p.SolicitacoesEstados)
                            .HasForeignKey(d => d.IdEstado)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("SolicitacoesEstados_SolicitacoesEstadosPC_FK");

            builder.HasOne(d => d.IdSolicitacaoNavigation)
                            .WithMany(p => p.SolicitacoesEstados)
                            .HasForeignKey(d => d.IdSolicitacao)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("SolicitacoesEstados_Solicitacao_FK");

        }
    }
}
