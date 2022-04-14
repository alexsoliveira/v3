using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping
{
    public class SolicitacoesEstadosPcMapping : IEntityTypeConfiguration<SolicitacoesEstadosPc>
    {
        public void Configure(EntityTypeBuilder<SolicitacoesEstadosPc> builder)
        {
            builder.HasKey(e => e.IdSolicitacaoEstado)
                  .HasName("SolicitacoesEstadosPCPK");
            builder.ToTable("SolicitacoesEstadosPC");
            builder.Property(e => e.IdSolicitacaoEstado).ValueGeneratedNever();
            builder.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);
        }
    }
}
