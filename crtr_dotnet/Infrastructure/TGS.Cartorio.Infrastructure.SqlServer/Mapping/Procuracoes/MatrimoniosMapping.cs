using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGS.Cartorio.Domain.Entities.Procuracoes;
using TGS.Cartorio.Infrastructure.SqlServer.Enumerable;

namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping.Procuracoes
{

    public class MatrimoniosMapping : IEntityTypeConfiguration<Matrimonios>
    {
        public void Configure(EntityTypeBuilder<Matrimonios> builder)
        {
            builder.ToTable("Matrimonios", Schemas.Procuracoes.ToString());

            builder.HasKey(e => e.IdMatrimonio)
                    .HasName("Procuracoes_MatrimoniosPK");

            builder.Property(e => e.IdMatrimonio)
                            .IsRequired();
            
            builder.Property(e => e.IdSolicitacao)
                            .IsRequired();

            builder.Property(e => e.CamposJson)
                            .IsRequired();

            builder.Property(e => e.DataOperacao)
                            .HasColumnType("datetime")
                            .HasDefaultValueSql("(getdate())")
                            .IsRequired();

            builder.Property(e => e.IdUsuario)
                            .IsRequired();

            builder.HasOne(d => d.IdSolicitacaoNavigation)
                            .WithMany(p => p.Matrimonios)
                            .HasForeignKey(d => d.IdSolicitacao)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("Procuracoes_MatrimoniosSolicitacoesFK")
                            .IsRequired();
        }
    }
}
