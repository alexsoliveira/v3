using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;
using System.Linq;
using System;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Entities.Procuracoes;

namespace TGS.Cartorio.Infrastructure.SqlServer.Context
{
    public class EFContext : DbContext, IUnitOfWork
    {

        public EFContext(DbContextOptions<EFContext> options) : base(options) {           
            
        }

        #region Propriedades
        public virtual DbSet<AssinaturaDigitalLog> AssinaturaDigitalLog { get; set; }
        public virtual DbSet<Cartorios> Cartorios { get; set; }
        public virtual DbSet<CartoriosContatos> CartoriosContatos { get; set; }
        public virtual DbSet<CartoriosEnderecos> CartoriosEnderecos { get; set; }
        public virtual DbSet<CartoriosModalidadesPc> CartoriosModalidadesPc { get; set; }
        public virtual DbSet<CartoriosTaxas> CartoriosTaxas { get; set; }
        public virtual DbSet<CartoriosServicosPc> CartoriosServicosPc { get; set; }
        public virtual DbSet<Contatos> Contatos { get; set; }
        public virtual DbSet<Enderecos> Enderecos { get; set; }
        public virtual DbSet<GenerosPc> GenerosPc { get; set; }
        public virtual DbSet<Perfis> Perfis { get; set; }
        public virtual DbSet<LogSistema> LogSistema { get; set; }
        public virtual DbSet<PerfisPermissoes> PerfisPermissoes { get; set; }
        public virtual DbSet<Pessoas> Pessoas { get; set; }
        public virtual DbSet<PessoasContatos> PessoasContatos { get; set; }
        public virtual DbSet<PessoasEnderecos> PessoasEnderecos { get; set; }
        public virtual DbSet<PessoasFisicas> PessoasFisicas { get; set; }
        public virtual DbSet<PessoasJuridicas> PessoasJuridicas { get; set; }
        public virtual DbSet<Produtos> Produtos { get; set; }
        public virtual DbSet<ProdutosCategoriasPc> ProdutosCategoriasPc { get; set; }
        public virtual DbSet<ProdutosImagens> ProdutosImagens { get; set; }
        public virtual DbSet<ProdutosModalidadesPc> ProdutosModalidadesPc { get; set; }
        public virtual DbSet<ProdutosDocumentos> ProdutosDocumentos { get; set; }
        public virtual DbSet<ProdutosModalidades> ProdutosModalidades { get; set; }
        public virtual DbSet<Solicitacoes> Solicitacoes { get; set; }
        public virtual DbSet<SolicitacoesDocumentos> SolicitacoesDocumentos { get; set; }
        public virtual DbSet<SolicitacoesEstadosPc> SolicitacoesEstadosPc { get; set; }
        public virtual DbSet<SolicitacoesTaxas> SolicitacoesTaxas { get; set; }
        public virtual DbSet<SolicitacoesEstados> SolicitacoesEstados { get; set; }
        public virtual DbSet<SolicitacoesNotificacoes> SolicitacoesNotificacoes { get; set; }
        public virtual DbSet<TaxasExtras> TaxasExtras { get; set; }
        public virtual DbSet<TiposContatosPc> TiposContatosPc { get; set; }
        public virtual DbSet<TiposDocumentosPc> TiposDocumentosPc { get; set; }        
        public virtual DbSet<TiposFretesPc> TiposFretesPc { get; set; }
        public virtual DbSet<TiposPartesPc> TiposPartesPc { get; set; }
        public virtual DbSet<TiposTaxasPc> TiposTaxasPc { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<UsuariosContatos> UsuariosContatos { get; set; }
        public virtual DbSet<UsuariosPerfis> UsuariosPerfis { get; set; }
        public virtual DbSet<CartoriosEstadosPc> CartoriosEstadosPc { get; set; }
        public virtual DbSet<SolicitacoesNotificacoesEstadosPc> SolicitacoesNotificacoesEstadosPc { get; set; }
        public virtual DbSet<Configuracoes> Configuracoes { get; set; }
        public virtual DbSet<UfsPc> UfsPc { get; set; }
        #endregion


        #region Procuracoes
        public DbSet<Matrimonios> Matrimonios { get; set; }
        public DbSet<MatrimoniosDocumentos> MatrimoniosDocumentos { get; set; }
        public DbSet<ProcuracoesPartes> ProcuracoesPartes { get; set; }
        public DbSet<ProcuracoesPartesEstadosPc> ProcuracoesPartesEstadosPc { get; set; }
        public DbSet<ProcuracoesPartesEstados> ProcuracoesPartesEstados { get; set; }
        public DbSet<TiposProcuracoesPartesPc> TiposProcuracoesPartesPc { get; set; }
        #endregion


        #region Escrituras
        #endregion


        #region Certidoes
        #endregion



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EFContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        public async Task<bool> Commit()
        {
            try
            {
                foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataOperacao") != null))
                {
                    entry.Property("DataOperacao").CurrentValue = DateTime.Now;
                }

                var ret = await base.SaveChangesAsync() > 0;

                foreach (var entry in ChangeTracker.Entries())
                {
                    entry.State = EntityState.Detached;
                }

                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
    }
}
