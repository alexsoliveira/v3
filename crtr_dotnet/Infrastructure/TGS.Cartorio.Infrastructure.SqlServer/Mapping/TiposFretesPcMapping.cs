using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping
{
    public class TiposFretesPcMapping : IEntityTypeConfiguration<TiposFretesPc>
    {
        public void Configure(EntityTypeBuilder<TiposFretesPc> builder)
        {
            builder.ToTable("TiposFretesPc");


            builder.HasKey(e => e.IdTipoFrete)
                   .HasName("TiposFretesPCPK");
            builder.Property(e => e.IdTipoFrete).ValueGeneratedNever();
            builder.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);
        }
    }
}
