using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGS.Cartorio.Domain.Entities;



namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping
{
    public class SolicitacoesNotificacoesEstadosPcMapping : IEntityTypeConfiguration<SolicitacoesNotificacoesEstadosPc>
    {

        public void Configure(EntityTypeBuilder<SolicitacoesNotificacoesEstadosPc> builder)
        {
            builder.ToTable("SolicitacoesNotificacoesEstadosPc");

            builder.HasKey(e => e.IdSolicitacaoNotificacaoEstado)
                .HasName("SolicitacoesNotificacoesEstadosPCPK");

            builder.ToTable("SolicitacoesNotificacoesEstadosPC");

            builder.Property(e => e.IdSolicitacaoNotificacaoEstado).ValueGeneratedNever();

            builder.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);
        }
    }
}
