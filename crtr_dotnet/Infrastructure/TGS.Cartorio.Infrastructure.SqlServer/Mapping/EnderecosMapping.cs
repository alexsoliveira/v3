using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping
{
   public class EnderecosMapping : IEntityTypeConfiguration<Enderecos>
    {
        public void Configure(EntityTypeBuilder<Enderecos> builder)
        {
            builder.ToTable("Enderecos");

            builder.HasKey(e => e.IdEndereco)
                   .HasName("EnderecosPK");

            builder.Property(e => e.Conteudo)
                .IsRequired()
                .HasMaxLength(4000);

            builder.Property(e => e.DataOperacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
				
			
            builder.Property(e => e.FlagAtivo)
                .IsRequired()
                .HasDefaultValueSql("((1))");            

            builder.HasOne(d => d.IdUsuarioNavigation)
                .WithMany(p => p.Enderecos)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Enderecos_Usuarios_FK");
        }
    }
}
