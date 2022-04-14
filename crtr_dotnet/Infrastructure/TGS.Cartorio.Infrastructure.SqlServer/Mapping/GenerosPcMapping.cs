using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping
{
   public class GenerosPcMapping : IEntityTypeConfiguration<GenerosPc>
    {
        public void Configure(EntityTypeBuilder<GenerosPc> builder)
        {

            builder.HasKey(e => e.IdGenero)
                   .HasName("GeneroPCPK");

            builder.ToTable("GenerosPC");

            builder.Property(e => e.IdGenero).ValueGeneratedNever();

            builder.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);
				
				
        }
    }
}
