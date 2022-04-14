using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping
{
    public class ProdutosModalidadesPcMapping : IEntityTypeConfiguration<ProdutosModalidadesPc>
    {
        public void Configure(EntityTypeBuilder<ProdutosModalidadesPc> builder)
        {
            builder.ToTable("ProdutosModalidadesPc");

            builder.HasKey(e => e.IdModalidade)
                    .HasName("ProdutoModalidadePCPK");

            builder.Property(e => e.IdModalidade).ValueGeneratedNever();

            builder.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.Titulo)
                .HasMaxLength(100)
                .IsUnicode(false);

        }
    }
}
