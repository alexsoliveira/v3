using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping
{
    public class TiposPartesPcMapping : IEntityTypeConfiguration<TiposPartesPc>
    {
        public void Configure(EntityTypeBuilder<TiposPartesPc> builder)
        {

            builder.ToTable("TiposPartesPc");

            builder.HasKey(e => e.IdTipoParte)
                  .HasName("TiposPartesPCPK");

            builder.ToTable("TiposPartesPc");
            builder.Property(e => e.IdTipoParte).ValueGeneratedNever();
            builder.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(2000)
                .IsUnicode(false);
        }
    }
}
