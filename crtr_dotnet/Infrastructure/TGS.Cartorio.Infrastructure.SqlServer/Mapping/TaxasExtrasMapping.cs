using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping
{
    public class TaxasExtrasMapping : IEntityTypeConfiguration<TaxasExtras>
    {
        public void Configure(EntityTypeBuilder<TaxasExtras> builder)
        {
            builder.HasKey(e => e.IdTaxaExtra)
                   .HasName("TaxasExtrasPK");

            builder.HasOne(e => e.UsuarioNavigation)
                .WithMany(e => e.TaxasExtras)
                .HasForeignKey(e => e.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TaxasExtrasUsuariosFK");

            builder.HasOne(e => e.TiposTaxasPcNavigation)
                .WithMany(e => e.TaxasExtras)
                .HasForeignKey(e => e.IdTipoTaxa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TaxasExtrasTiposTaxasPCFK");

        }
    }
}
