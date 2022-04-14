using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping
{
    public class PessoasMapping : IEntityTypeConfiguration<Pessoas>
    {
        public void Configure(EntityTypeBuilder<Pessoas> builder)
        {
            builder.ToTable("Pessoas");
            
            builder.HasKey(e => e.IdPessoa)
                    .HasName("PessoasPK");

            builder.HasIndex(e => e.Documento)
                .HasName("UK_Pessoas")
                .IsUnique();

            builder.Property(e => e.DataOperacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.FlagAtivo).HasDefaultValueSql("((1))");

            builder.HasOne(d => d.IdTipoDocumentoNavigation)
                .WithMany(p => p.Pessoas)
                .HasForeignKey(d => d.IdTipoDocumento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Pessoas_TiposDocumentosPC_FK");

            builder.HasOne(d => d.IdUsuarioNavigation)
                .WithMany(p => p.Pessoas)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Pessoas_Usuarios_FK");

        }
    }
}
