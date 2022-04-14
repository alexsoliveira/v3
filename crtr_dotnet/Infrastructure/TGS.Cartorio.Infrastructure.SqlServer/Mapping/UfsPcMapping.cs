using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping
{
    public class UfsPcMapping : IEntityTypeConfiguration<UfsPc>
    {
        public void Configure(EntityTypeBuilder<UfsPc> builder)
        {
            builder.HasKey(e => e.IdUf)
                    .HasName("UFspk");

            builder.ToTable("UFsPC");

            builder.Property(e => e.IdUf).HasColumnName("IdUF");

            builder.Property(e => e.DescricaoUf)
                .IsRequired()
                .HasColumnName("DescricaoUF")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Uf)
                .IsRequired()
                .HasColumnName("UF")
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
        }
    }
}
