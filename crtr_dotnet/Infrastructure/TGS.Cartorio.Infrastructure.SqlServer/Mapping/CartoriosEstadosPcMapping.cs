using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping
{
    public class CartoriosEstadosPcMapping : IEntityTypeConfiguration<CartoriosEstadosPc>
    {
        public void Configure(EntityTypeBuilder<CartoriosEstadosPc> builder)
        {
            builder.ToTable("CartoriosEstadosPc");

            builder.HasKey(e => e.IdCartorioEstado)
                      .HasName("CartoriosEstadosPCPK");

            builder.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(2000)
                .IsUnicode(false);
        }
    }
}
