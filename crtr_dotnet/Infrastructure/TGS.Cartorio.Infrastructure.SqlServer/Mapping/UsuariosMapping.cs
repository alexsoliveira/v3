using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping
{
    public class UsuariosMapping : IEntityTypeConfiguration<Usuarios>
    {
        public void Configure(EntityTypeBuilder<Usuarios> builder)
        {
            builder.ToTable("Usuarios");

            builder.HasKey(e => e.IdUsuario)
                    .HasName("UsuariosPK");

            builder.HasIndex(e => e.Email)
                .HasName("UK_Usuarios")
                .IsUnique();

            builder.Property(e => e.DataOperacao).HasColumnType("datetime");

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.NomeUsuario)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.HasOne(d => d.IdPessoaNavigation)
                .WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdPessoa)
                .HasConstraintName("Usuarios_Pessoas_FK");
        }
    }
}
