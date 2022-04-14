using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping
{
    public class PessoasJuridicasMapping : IEntityTypeConfiguration<PessoasJuridicas>
    {
        public void Configure(EntityTypeBuilder<PessoasJuridicas> builder)
        {
            builder.ToTable("PessoasJuridicas");

            builder.HasKey(e => e.IdPessoaJuridica)
                    .HasName("PessoasJuridicaPK");

            builder.HasIndex(e => e.IdPessoa)
                .HasName("UK_PessoasJuridicas")
                .IsUnique();

            builder.Property(e => e.DataOperacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.NomeFantasia)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.RazaoSocial)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.HasOne(d => d.IdGeneroNavigation)
                .WithMany(p => p.PessoasJuridicas)
                .HasForeignKey(d => d.IdGenero)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PessoasJuridicas_GeneroPC_FK");

            builder.HasOne(d => d.IdPessoaNavigation)
                .WithOne(p => p.PessoasJuridicas)
                .HasForeignKey<PessoasJuridicas>(d => d.IdPessoa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PessoasJuridicas_Pessoas_FK");

            builder.HasOne(d => d.IdUsuarioNavigation)
                .WithMany(p => p.PessoasJuridicas)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PessoasJuridicas_Usuarios_FK");

        }
    }
}
