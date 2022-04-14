using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping
{
    public class CartoriosModalidadesPcMapping : IEntityTypeConfiguration<CartoriosModalidadesPc>
    {

        public void Configure(EntityTypeBuilder<CartoriosModalidadesPc> builder)
        {
            builder.ToTable("CartoriosModalidadesPC");
            builder.HasKey(e => e.IdCartorioModalidade);
            builder.Property(e => e.IdCartorioModalidade).ValueGeneratedNever();
            builder.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);
        }
    }
}
