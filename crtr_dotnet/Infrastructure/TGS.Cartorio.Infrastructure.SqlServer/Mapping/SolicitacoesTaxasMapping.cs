using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping
{
    public class SolicitacoesTaxasMapping : IEntityTypeConfiguration<SolicitacoesTaxas>
    {
        public void Configure(EntityTypeBuilder<SolicitacoesTaxas> builder)
        {
            builder.ToTable("SolicitacoesTaxas");

            builder.HasKey(e => e.IdSolicitacaoTaxa)
                    .HasName("PK_SolicitacoesTaxas");

            builder.HasOne(p => p.SolicitacoesNavigation)
                .WithMany(p => p.SolicitacoesTaxas)
                .HasForeignKey(p => p.IdSolicitacao)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SolicitacoesSolicitacoesTaxasFK");

            builder.HasOne(p => p.TaxasExtrasNavigation)
                .WithMany(p => p.SolicitacoesTaxas)
                .HasForeignKey(p => p.IdTaxaExtra)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SolicitacoesTaxasTaxasExtrasFK");

            builder.HasOne(p => p.CartoriosTaxasNavigation)
                .WithMany(p => p.SolicitacoesTaxas)
                .HasForeignKey(p => p.IdCartorioTaxa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SolicitacoesTaxasCartoriosTaxasFK");
        }
    }
}
