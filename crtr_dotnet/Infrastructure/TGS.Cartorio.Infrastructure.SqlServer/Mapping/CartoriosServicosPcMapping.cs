using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping
{
    public class CartoriosServicosPcMapping : IEntityTypeConfiguration<CartoriosServicosPc>
    {
        

        public void Configure(EntityTypeBuilder<CartoriosServicosPc> builder)
        {
            builder.HasKey(e => e.IdCartorioServicos)
                    .HasName("PK_CartoriosServicosPC_1");
        }
    }
}
