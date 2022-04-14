using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping
{
    public class LogSistemaMapping : IEntityTypeConfiguration<LogSistema>
    {
        public void Configure(EntityTypeBuilder<LogSistema> builder)
        {
            builder.ToTable("LogSistema");

            builder.HasKey(e => e.IdLogSistema)
                   .HasName("LogSistemaPK");

            builder.Property(e => e.IdLogSistema).ValueGeneratedOnAdd();

            builder.Property(e => e.DataOperacao)
                .IsRequired()
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.CodLogSistema)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false);

            builder.Property(e => e.JsonConteudo)
                .IsRequired()
                .IsUnicode(false);
        }
    }
}
