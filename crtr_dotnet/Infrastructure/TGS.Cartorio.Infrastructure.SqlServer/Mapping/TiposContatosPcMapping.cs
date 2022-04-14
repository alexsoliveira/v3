using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping
{
    public class TiposContatosPcMapping : IEntityTypeConfiguration<TiposContatosPc>
    {
        public void Configure(EntityTypeBuilder<TiposContatosPc> builder)
        {
            builder.HasKey(e => e.IdTipoContato)
                   .HasName("TiposContatosPCPK");

            builder.ToTable("TiposContatosPC");
            builder.Property(e => e.IdTipoContato).ValueGeneratedNever();

            builder.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

        }
    }
}
