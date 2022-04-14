using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGS.Cartorio.Domain.Entities.Procuracoes;
using TGS.Cartorio.Infrastructure.SqlServer.Enumerable;

namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping.Procuracoes
{

    public class ProcuracoesPartesEstadosPcMapping : IEntityTypeConfiguration<ProcuracoesPartesEstadosPc>
    {
        public void Configure(EntityTypeBuilder<ProcuracoesPartesEstadosPc> builder)
        {
            builder.ToTable("ProcuracoesPartesEstadosPC", Schemas.Procuracoes.ToString());

            builder.HasKey(e => e.IdProcuracaoParteEstado)
                    .HasName("Procuracoes_ProcuracoesPartesEstadosPCPK");

            builder.Property(e => e.IdProcuracaoParteEstado)
                            .IsRequired();
            builder.Property(e => e.Descricao)
                            .HasMaxLength(200)
                            .IsRequired();

            builder.Property(e => e.FlagAtivo)
                            .IsRequired();

            builder.Property(e => e.NuOrdem)
                            .IsRequired();
        }
    }
}
