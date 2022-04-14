using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGS.Cartorio.Domain.Entities.Procuracoes;
using TGS.Cartorio.Infrastructure.SqlServer.Enumerable;

namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping.Procuracoes
{

    public class ProcuracoesPartesEstadosMapping : IEntityTypeConfiguration<ProcuracoesPartesEstados>
    {
        public void Configure(EntityTypeBuilder<ProcuracoesPartesEstados> builder)
        {
            builder.ToTable("ProcuracoesPartesEstados", Schemas.Procuracoes.ToString());

            builder.HasKey(e => e.IdProcuracaoParteEstado)
                    .HasName("Procuracoes_ProcuracoesPartesEstadosPK");

            builder.Property(e => e.IdProcuracaoParteEstado)
                            .IsRequired();

            builder.Property(e => e.DataOperacao)
                   .HasColumnType("datetime")
                   .HasDefaultValueSql("(getdate())")
                   .IsRequired();

            builder.HasOne(d => d.IdProcuracaoParteEstadoPcNavigation)
                .WithMany(p => p.ProcuracoesPartesEstados)
                .HasForeignKey(d => d.IdProcuracaoParteEstadoPc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Procuracoes_ProcuracoesPartesEstados_ProcuracoesPartesEstadosPC_FK");

            builder.HasOne(d => d.IdProcuracaoParteNavigation)
                .WithMany(p => p.ProcuracoesPartesEstadosNavigation)
                .HasForeignKey(d => d.IdProcuracaoParte)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Procuracoes_ProcuracoesPartesEstados_ProcuracoesPartes_FK");
        }
    }
}
