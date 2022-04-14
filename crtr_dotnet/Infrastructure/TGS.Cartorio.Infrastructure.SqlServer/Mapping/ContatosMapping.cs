using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping
{
    public class ContatosMapping : IEntityTypeConfiguration<Contatos>
    {
        public void Configure(EntityTypeBuilder<Contatos> builder)
        {
            builder.ToTable("Contatos");

            builder.HasKey(e => e.IdContato)
                   .HasName("ContatosPK");

            builder.Property(e => e.Conteudo)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(e => e.DataOperacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.FlagAtivo)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            builder.HasOne(d => d.IdUsuarioNavigation)
                .WithMany(p => p.Contatos)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Contatos_Usuarios_FK");
        }
    }
}
