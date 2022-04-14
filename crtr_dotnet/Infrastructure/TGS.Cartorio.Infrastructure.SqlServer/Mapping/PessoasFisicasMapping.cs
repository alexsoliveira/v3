using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping
{
    public class PessoasFisicasMapping : IEntityTypeConfiguration<PessoasFisicas>
    {
        public void Configure(EntityTypeBuilder<PessoasFisicas> builder)
        {
            builder.ToTable("PessoasFisicas");


            builder.HasKey(e => e.IdPessoaFisica)
                   .HasName("PessoasFisicasPK");

            builder.HasIndex(e => e.IdPessoa)
                .HasName("UK_PessoasFisicas")
                .IsUnique();

            builder.Property(e => e.DataOperacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.NomePessoa)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.NomeSocial)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.HasOne(d => d.IdGeneroNavigation)
                .WithMany(p => p.PessoasFisicas)
                .HasForeignKey(d => d.IdGenero)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PessoasFisicas_GeneroPC_FK");

            builder.HasOne(d => d.IdPessoaNavigation)
                .WithOne(p => p.PessoasFisicas)
                .HasForeignKey<PessoasFisicas>(d => d.IdPessoa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PessoasFisicas_Pessoas_FK");

            builder.HasOne(d => d.IdUsuarioNavigation)
                .WithMany(p => p.PessoasFisicas)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PessoasFisicas_Usuarios_FK");


        }
    }
}
