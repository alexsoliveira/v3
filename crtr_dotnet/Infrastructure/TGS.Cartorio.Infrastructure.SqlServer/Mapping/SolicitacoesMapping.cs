using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Infrastructure.SqlServer.Mapping
{
    public class SolicitacoesMapping : IEntityTypeConfiguration<Solicitacoes>
    {
        public void Configure(EntityTypeBuilder<Solicitacoes> builder)
        {
            builder.ToTable("Solicitacoes");

            builder.HasKey(e => e.IdSolicitacao)
                    .HasName("SolicitacaoPK");

            builder.Property(e => e.CamposPagamento).HasMaxLength(200);
            
            builder.Property(e => e.DataOperacao)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");

            builder.Property(e => e.ValorFrete).HasColumnType("decimal(18, 2)");

            builder.HasOne(d => d.IdCartorioNavigation)
                .WithMany(p => p.Solicitacoes)
                .HasForeignKey(d => d.IdCartorio)
                .HasConstraintName("Solicitacoes_Cartorios_FK");

            builder.HasOne(d => d.IdProdutoNavigation)
                .WithMany(p => p.Solicitacoes)
                .HasForeignKey(d => d.IdProduto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Solicitacoes_Produtos_FK");

            builder.HasOne(d => d.IdSolicitacaoEstadoNavigation)
                .WithMany(p => p.Solicitacoes)
                .HasForeignKey(d => d.IdSolicitacaoEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Solicitacoes_SolicitacoesEstadosPC_FK");

            builder.HasOne(d => d.IdTipoFreteNavigation)
                .WithMany(p => p.Solicitacoes)
                .HasForeignKey(d => d.IdTipoFrete)
                .HasConstraintName("Solicitacoes_TiposFretesPC_FK");

            builder.HasOne(d => d.IdUsuarioNavigation)
                .WithMany(p => p.Solicitacoes)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Solicitacoes_Usuarios_FK");

            builder.HasOne(d => d.IdPessoaSolicitanteNavigation)
                .WithMany(p => p.Solicitacoes)
                .HasForeignKey(d => d.IdPessoa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("SolicitacoesPessoasFK");
        }
    }
}
