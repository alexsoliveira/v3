using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Enumerables;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Infrastructure.SqlServer.Context;

namespace TGS.Cartorio.Infrastructure.SqlServer.Repositories
{
    public class SolicitacoesSqlRepository : ISolicitacoesSqlRepository
    {
        private readonly EFContext _context;
        private readonly int _tamanhoPagina;

        public SolicitacoesSqlRepository(EFContext context, IConfiguration configuration)
        {
            _context = context;
            _tamanhoPagina = configuration.GetValue("SqlServer:TamanhoPagina", 50);
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task Incluir(Solicitacoes solicitacao)
        {
            _context.Solicitacoes.Add(solicitacao);
            await _context.Commit();
        }

        public async Task<IEnumerable<Solicitacoes>> TodasSolicitacoesAguardandoPagamentoBoleto()
        {
            try
            {
                var dataVencimentoBoletoAtual = $"DataVencimentoBoleto\":\"{DateTime.Now.ToString("yyyy-MM-dd")}";
                var dataVencimentoBoletoCom_Um_Dia = $"DataVencimentoBoleto\":\"{DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd")}";
                var dataVencimentoBoletoCom_Dois_Dias = $"DataVencimentoBoleto\":\"{DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd")}";
                var dataVencimentoBoletoCom_Tres_Dias = $"DataVencimentoBoleto\":\"{DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd")}";
                var dataVencimentoBoletoCom_Quatro_Dias = $"DataVencimentoBoleto\":\"{DateTime.Now.AddDays(-4).ToString("yyyy-MM-dd")}";
                var dataVencimentoBoletoCom_Cinco_Dias = $"DataVencimentoBoleto\":\"{DateTime.Now.AddDays(-5).ToString("yyyy-MM-dd")}";
                var dataVencimentoBoletoCom_Seis_Dias = $"DataVencimentoBoleto\":\"{DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd")}";
                var dataVencimentoBoletoCom_Sete_Dias = $"DataVencimentoBoleto\":\"{DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd")}";

                return await _context.Solicitacoes.AsNoTracking()
                                                  .Where(x => x.IdSolicitacaoEstado == 7 //AGUARDANDO EFETUAR PAGAMENTO
                                                           && x.Conteudo.Contains("TipoPagamentoAtual\":\"Boleto")
                                                          && (x.CamposPagamento.Contains(dataVencimentoBoletoAtual)
                                                           || x.CamposPagamento.Contains(dataVencimentoBoletoCom_Um_Dia)
                                                           || x.CamposPagamento.Contains(dataVencimentoBoletoCom_Dois_Dias)
                                                           || x.CamposPagamento.Contains(dataVencimentoBoletoCom_Tres_Dias)
                                                           || x.CamposPagamento.Contains(dataVencimentoBoletoCom_Quatro_Dias)
                                                           || x.CamposPagamento.Contains(dataVencimentoBoletoCom_Cinco_Dias)
                                                           || x.CamposPagamento.Contains(dataVencimentoBoletoCom_Seis_Dias)
                                                           || x.CamposPagamento.Contains(dataVencimentoBoletoCom_Sete_Dias))
                                                        ).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Atualizar(Solicitacoes solicitacao)
        {
            try
            {
                solicitacao.DataOperacao = DateTime.Now;
                _context.Solicitacoes.Update(solicitacao);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AtualizarViaJob(Solicitacoes solicitacao)
        {
            try
            {
                solicitacao.DataOperacao = DateTime.Now;
                _context.Solicitacoes.Update(solicitacao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void FinalizarJob()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<StatusSolicitacaoHeader> BuscarDadosStatusSolicitacao(long idsolicitacao)
        {
            try
            {
                return await _context.Solicitacoes
                                     .Include(p => p.IdPessoaSolicitanteNavigation)
                                     .Include(p => p.IdProdutoNavigation)
                                     .Join(_context.Usuarios, 
                                               s => s.IdPessoa, 
                                               u => u.IdPessoa,
                                          (s, u) => new { Solicitacao = s, Usuario = u})
                                     .Where(p => p.Solicitacao.IdSolicitacao == idsolicitacao)
                                     .Select(s => new StatusSolicitacaoHeader { 
                                        IdProduto = s.Solicitacao.IdProduto,
                                        IdSolicitacao = s.Solicitacao.IdSolicitacao,
                                        Produto = s.Solicitacao.IdProdutoNavigation.Titulo,
                                        Solicitante = s.Usuario.NomeUsuario
                                     })?.FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Solicitacoes>> BuscarTodos(Expression<Func<Solicitacoes, bool>> func, int pagina = 0)
        {
            return await _context.Solicitacoes
                            .Include(i => i.SolicitacoesEstados)
                            .ThenInclude(x => x.IdEstadoNavigation)
                            .Where(func)                                                  
                            .ToListAsync();
        }

        public async Task<List<Solicitacoes>> BuscarTodosComNoLock(Expression<Func<Solicitacoes, bool>> func, int pagina = 0)
        {
            try
            {
                return await _context.Solicitacoes
                            .Include(x => x.IdPessoaSolicitanteNavigation)
                                .ThenInclude(x => x.PessoasContatos)
                                    .ThenInclude(x => x.IdContatoNavigation)
                            .Include(x => x.IdPessoaSolicitanteNavigation)
                                .ThenInclude(x => x.PessoasFisicas)
                            .AsNoTracking()
                            .Where(func)
                            .ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Solicitacoes> BuscarPorSolicitacoesProntasParaEnvioCartorio()
        {
            try
            {
                return _context.Solicitacoes
                            .AsNoTracking()
                            .Include(x => x.IdPessoaSolicitanteNavigation)
                                .ThenInclude(x => x.PessoasContatos)
                                    .ThenInclude(x => x.IdContatoNavigation)
                            .Include(x => x.IdPessoaSolicitanteNavigation)
                                .ThenInclude(x => x.PessoasFisicas)
                            .Where(sol => sol.IdSolicitacaoEstado == (int)ESolicitacoesEstadosPC.Solicitacao_pronta_para_envio_ao_cartorio)
                            .ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Dispose()
        {
            ////_context?.Dispose();
        }

        public async Task<Solicitacoes> BuscarId(long id)
        {
            try
            {
                var _retorno = await _context.Solicitacoes
                    .AsNoTracking()
                    .Include(i => i.IdProdutoNavigation)
                    .Include(i => i.IdUsuarioNavigation)
                    .Include(i => i.IdSolicitacaoEstadoNavigation)
                    .Include(i => i.IdPessoaSolicitanteNavigation)
                    .FirstOrDefaultAsync(p => p.IdSolicitacao == id);

                return _retorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<List<MinhasSolicitacoes>> MinhasSolicitacoes(long idPessoa)
        {
            try
            {
                return await _context.Solicitacoes
                .Include(i => i.IdProdutoNavigation)
                .Include(i => i.IdSolicitacaoEstadoNavigation)
                .Include(i => i.SolicitacoesEstados)
                .Where(p => p.IdPessoa == idPessoa)
                .Select(c => new MinhasSolicitacoes
                {
                    IdSolicitacao = c.IdSolicitacao,
                    IdPessoaSolicitante = c.IdPessoa.Value,
                    Participacao = "Solicitante",
                    Produto = c.IdProdutoNavigation.Titulo,
                    CamposPagamento = c.CamposPagamento,
                    Conteudo = c.Conteudo,
                    DataSolicitacao = c.DataOperacao,
                    Estado = c.IdSolicitacaoEstadoNavigation.Descricao,
                    UltimaInteracao = c.SolicitacoesEstados.OrderByDescending(x => x.DataOperacao).First().DataOperacao
                })
                .OrderByDescending(x => x.DataSolicitacao)
                .ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
                                        
        }

        public async Task<MinhaSolicitacao> ConsultarBoleto(long idSolicitacao)
        {
            try
            {
                var solicitacao = await _context.Solicitacoes
                .AsNoTracking()
                .Include(p => p.IdPessoaSolicitanteNavigation)
                    .ThenInclude(p => p.IdUsuarioNavigation)
                .Include(p => p.IdPessoaSolicitanteNavigation)
                    .ThenInclude(p => p.PessoasContatos)
                        .ThenInclude(p => p.IdContatoNavigation)
                .Include(p => p.ProcuracoesPartes)
                    .ThenInclude(p => p.TiposProcuracoesPartesNavigation)
                .FirstOrDefaultAsync(p => p.IdSolicitacao == idSolicitacao);

                return new MinhaSolicitacao
                {
                    CamposPagamento = solicitacao.CamposPagamento,
                    PessoaSolicitante = solicitacao.IdPessoaSolicitanteNavigation,
                    EnderecoPagador = solicitacao.ProcuracoesPartes.FirstOrDefault(x => x.IdPessoa == solicitacao.IdPessoa)?.EnderecoEntrega,
                    Solicitacao = solicitacao
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task RegistrarNovoBoleto(long id)
        {
            try
            {
                await _context.Solicitacoes
                .Where(p => p.IdSolicitacao == id)
                .Select(c => new MinhaSolicitacao
                {
                    CamposPagamento = c.CamposPagamento,
                })
                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        
    }
}