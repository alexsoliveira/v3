using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGS.Cartorio.Domain.Entities.Procuracoes;
using TGS.Cartorio.Infrastructure.SqlServer.Enumerable;

namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping.Procuracoes
{

    public class TiposProcuracoesPartesPcMapping : IEntityTypeConfiguration<TiposProcuracoesPartesPc>
    {
        public void Configure(EntityTypeBuilder<TiposProcuracoesPartesPc> builder)
        {
            builder.ToTable("TiposProcuracoesPartesPC", Schemas.Procuracoes.ToString());

            builder.HasKey(e => e.IdTipoProcuracaoParte)
                    .HasName("Procuracoes_TiposProcuracoesPartesPCPK");

            builder.Property(e => e.IdTipoProcuracaoParte)
                    .IsRequired();

            builder.Property(e => e.Descricao)
                    .HasMaxLength(200)
                    .IsRequired();
        }
    }
}
