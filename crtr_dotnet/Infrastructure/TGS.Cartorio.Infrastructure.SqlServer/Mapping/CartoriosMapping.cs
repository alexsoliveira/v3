using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping
{
    public class CartoriosMapping : IEntityTypeConfiguration<Cartorios>
    {

        public void Configure(EntityTypeBuilder<Cartorios> builder)
        {
            builder.ToTable("Cartorios");

            builder.HasKey(e => e.IdCartorio);

            builder.Property(e => e.DataOperacao).HasColumnType("datetime");

            builder.HasOne(d => d.IdCartorioEstadoNavigation)
                .WithMany(p => p.Cartorios)
                .HasForeignKey(d => d.IdCartorioEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Cartorios_CartoriosEstadosPC_FK");

            builder.HasOne(d => d.IdCartorioModalidadeNavigation)
                .WithMany(p => p.Cartorios)
                .HasForeignKey(d => d.IdCartorioModalidade)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Cartorios_CartoriosModalidadesPC_FK");

            builder.HasOne(d => d.IdPessoaNavigation)
                .WithMany(p => p.Cartorios)
                .HasForeignKey(d => d.IdPessoa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Cartorios_Pessoas_FK");

            builder.HasOne(d => d.IdUsuarioNavigation)
                .WithMany(p => p.Cartorios)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Cartorios_Usuarios_FK");
        }
    }
}
