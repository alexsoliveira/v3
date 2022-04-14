using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping
{
    public class ConfiguracoesMapping : IEntityTypeConfiguration<Configuracoes>
    {
        

        public void Configure(EntityTypeBuilder<Configuracoes> builder)
        {
            builder.HasKey(e => e.IdConfiguracao)
                    .HasName("ConfiguracoesPK");

            builder.HasIndex(e => e.Descricao)
                .HasName("UK_Configuracoes")
                .IsUnique();

            builder.Property(e => e.Conteudo)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.DataOperacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(5000)
                .IsUnicode(false);

            builder.HasOne(d => d.IdUsuarioNavigation)
                .WithMany(p => p.Configuracoes)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ConfiguracoesUsuariosFK");
        }
    }
}
