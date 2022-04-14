using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping
{
   public class PessoasContatosMapping : IEntityTypeConfiguration<PessoasContatos>
    {
        public void Configure(EntityTypeBuilder<PessoasContatos> builder)
        {
            builder.ToTable("PessoasContatos");

            builder.HasKey(e => e.IdPessoaContato)
                   .HasName("PessoasContatosPK");

            builder.Property(e => e.DataOperacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.FlagAtivo).HasDefaultValueSql("((1))");

            builder.HasOne(d => d.IdContatoNavigation)
                    .WithMany(p => p.PessoasContatos)
                    .HasForeignKey(d => d.IdContato)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PessoasContatos_Contatos_FK");

            builder.HasOne(d => d.IdPessoaNavigation)
                .WithMany(p => p.PessoasContatos)
                .HasForeignKey(d => d.IdPessoa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PessoasContatos_Pessoas_FK");

            builder.HasOne(d => d.IdUsuarioNavigation)
                .WithMany(p => p.PessoasContatos)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PessoasContatos_Usuarios_FK");
        }
    }
}
