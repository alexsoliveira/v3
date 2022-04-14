using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping
{
    public class PessoasEnderecosMapping : IEntityTypeConfiguration<PessoasEnderecos>
    {
        public void Configure(EntityTypeBuilder<PessoasEnderecos> builder)
        {
            builder.ToTable("PessoasEnderecos");

            builder.HasKey(e => e.IdPessoaEndereco)
                   .HasName("PessoasEnderecosPK");

            builder.Property(e => e.DataOperacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.FlagAtivo).HasDefaultValueSql("((1))");

            builder.HasOne(d => d.IdEnderecoNavigation)
                .WithMany(p => p.PessoasEnderecos)
                .HasForeignKey(d => d.IdEndereco)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PessoasEnderecos_Enderecos_FK");

            builder.HasOne(d => d.IdUsuarioNavigation)
                .WithMany(p => p.PessoasEnderecos)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PessoasEnderecos_Usuarios_FK");
        }
    }
}
