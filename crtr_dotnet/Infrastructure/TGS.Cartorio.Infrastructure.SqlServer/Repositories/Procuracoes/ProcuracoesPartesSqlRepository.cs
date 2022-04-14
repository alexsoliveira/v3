using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities.Procuracoes;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer.Procuracoes;
using TGS.Cartorio.Infrastructure.SqlServer.Context;
using Microsoft.EntityFrameworkCore;
using TGS.Cartorio.Domain.Entities;
using System.Linq.Expressions;

namespace TGS.Cartorio.Infrastructure.SqlServer.Repositories.Procuracoes
{
    public class ProcuracoesPartesSqlRepository : IProcuracoesPartesSqlRepository
    {
        private readonly EFContext _context;
        private readonly int _tamanhoPagina;

        public ProcuracoesPartesSqlRepository(EFContext context, IConfiguration configuration)
        {
            _context = context;
            _tamanhoPagina = configuration.GetValue("SqlServer:TamanhoPagina", 50);
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<List<ProcuracoesPartes>> MinhasParticipacoes(long id)
        {
            try
            {
                return await _context.ProcuracoesPartes                
                    .Include(p => p.SolicitacoesNavigation)
                        .ThenInclude(p => p.IdProdutoNavigation)
                    .Include(p => p.SolicitacoesNavigation)
                        .ThenInclude(p => p.IdSolicitacaoEstadoNavigation)
                    .Include(p => p.SolicitacoesNavigation)
                        .ThenInclude(p => p.SolicitacoesEstados)                
                .Where(p => p.IdPessoa == id && p.IdTipoProcuracaoParte == 2)
                .Select(c => new ProcuracoesPartes
                {
                    IdSolicitacao = c.IdSolicitacao,
                    IdUsuario = c.IdUsuario,
                    SolicitacoesNavigation = c.SolicitacoesNavigation
                })
                .ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<List<StatusSolicitacao>> BuscarEstadoDoPedidoPorParticipante(long idSolicitacao)
        {
            try
            {
                return await _context.ProcuracoesPartes
                    .Include(i => i.PessoasNavigation)
                    .Include(i => i.TiposProcuracoesPartesNavigation)
                    .Include(i => i.ProcuracoesPartesEstadosNavigation)
                        .ThenInclude(i => i.IdProcuracaoParteEstadoPcNavigation)
                    .Join(_context.Usuarios, pp => pp.IdPessoa, u => u.IdPessoa, (pp, u) => new
                    {
                        ProcuracoesPartes = pp,
                        Usuarios = u
                    })
                    .Where(p => p.ProcuracoesPartes.IdSolicitacao == idSolicitacao
                        && p.ProcuracoesPartes.IdTipoProcuracaoParte == 2) //outorgantes
                    .Select(c => new StatusSolicitacao
                    {
                        IdSolicitacao = c.ProcuracoesPartes.IdSolicitacao,
                        IdPessoa = c.ProcuracoesPartes.IdPessoa,
                        Participante = c.Usuarios.NomeUsuario,
                        Documento = c.ProcuracoesPartes.PessoasNavigation.Documento,
                        IdTipo = c.ProcuracoesPartes.TiposProcuracoesPartesNavigation.IdTipoProcuracaoParte,
                        Tipo = c.ProcuracoesPartes.TiposProcuracoesPartesNavigation.Descricao,
                        IdEstadoParticipante = c.ProcuracoesPartes.ProcuracoesPartesEstadosNavigation.OrderByDescending(p => p.DataOperacao).FirstOrDefault().IdProcuracaoParteEstadoPc,
                        EstadoParticipante = c.ProcuracoesPartes.ProcuracoesPartesEstadosNavigation.OrderByDescending(p => p.DataOperacao).FirstOrDefault().IdProcuracaoParteEstadoPcNavigation.Descricao,
                        UltimaInteracao = c.ProcuracoesPartes.DataOperacao
                    })
                    .OrderBy(x => x.UltimaInteracao)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Atualizar(ProcuracoesPartes parte)
        {
            try
            {
                _context.Attach(parte).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ProcuracoesPartes> BuscarPorSolicitante(long idSolicitacao, long idPessoaSolicitante)
        {
            try
            {
                return await _context.ProcuracoesPartes.FirstOrDefaultAsync(x => x.IdSolicitacao == idSolicitacao 
                                                                              && x.IdPessoa == idPessoaSolicitante);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Remover(ProcuracoesPartes parte)
        {
            try
            {
                _context.ProcuracoesPartes.Remove(parte).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<ProcuracoesPartesEstadosPc>> BuscarTodasProcuracoesPartesEstados(Expression<Func<ProcuracoesPartesEstadosPc, bool>> func, int pagina = 0)
        {
            try
            {
                return await _context.ProcuracoesPartesEstadosPc
                            .Where(func)
                            .Skip(pagina * _tamanhoPagina)
                            .Take(_tamanhoPagina)
                            .OrderBy(p => p.NuOrdem)
                            .Select(p => new ProcuracoesPartesEstadosPc
                            {
                                IdProcuracaoParteEstado = p.IdProcuracaoParteEstado,
                                Descricao = p.Descricao
                            })
                            .ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }                         
        }

        public async Task Incluir(ProcuracoesPartes procuracoesParte)
        {
            try
            {
                _context.ProcuracoesPartes.Add(procuracoesParte);
                await _context.Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ProcuracoesPartes> BuscarPorId(long id)
        {
            try
            {
                return await _context.ProcuracoesPartes.FindAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> ValidarSeExisteProcuracaoParteComMatrimonio(long idPessoaSolicitante, long idMatrimonio)
        {
            try
            {
                var matrimonio = await _context.Matrimonios.FirstOrDefaultAsync(x => x.IdMatrimonio == idMatrimonio);
                if (matrimonio == null)
                    throw new Exception("Não foi possível localizar o matrimônio.");

                var procuracaoParte = await _context.ProcuracoesPartes.FirstOrDefaultAsync(x => x.IdPessoa == idPessoaSolicitante 
                                                                                             && x.IdSolicitacao == matrimonio.IdSolicitacao);

                if (procuracaoParte == null || procuracaoParte.IdSolicitacao <= 0)
                    throw new Exception("Não foi possível localizar a parte responsável da Solicitação.");

                return matrimonio.IdMatrimonio == idMatrimonio;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ProcuracoesPartes>> BuscarPorIdSolicitacao(long idSolicitacao)
        {
            try
            {
                return await _context.ProcuracoesPartes
                    .AsNoTracking()
                    .Include(p => p.PessoasNavigation)
                        .ThenInclude(p => p.PessoasFisicas)
                    .Include(p => p.PessoasNavigation)
                        .ThenInclude(p => p.PessoasContatos)
                    .Include(p => p.PessoasNavigation)
                        .ThenInclude(p => p.PessoasContatos)
                            .ThenInclude(p => p.IdContatoNavigation)
                    //.Include(p => p.MatrimoniosDocumentos)
                    //.Include(p => p.SolicitacoesNavigation)
                    //    .ThenInclude(p => p.Matrimonios)
                    //        .ThenInclude(p => p.MatrimoniosDocumentos)
                    .Join(_context.Usuarios, 
                          p => p.IdPessoa, 
                          u => u.IdPessoa,
                          (p, u) => new { ProcuracoesPartes = p, Usuarios = u })
                    .Where(x => x.ProcuracoesPartes.IdSolicitacao == idSolicitacao)
                    .Select(x => new ProcuracoesPartes { 
                        Email = x.ProcuracoesPartes.Email,
                        EnderecoEntrega = x.ProcuracoesPartes.EnderecoEntrega,
                        IdPessoa = x.ProcuracoesPartes.IdPessoa,
                        DataOperacao = x.ProcuracoesPartes.DataOperacao,
                        IdProcuracaoParte = x.ProcuracoesPartes.IdProcuracaoParte,
                        IdProcuracaoParteEstado = x.ProcuracoesPartes.IdProcuracaoParteEstado,
                        IdSolicitacao = x.ProcuracoesPartes.IdSolicitacao,
                        IdTipoProcuracaoParte = x.ProcuracoesPartes.IdTipoProcuracaoParte,
                        JsonConteudo = x.ProcuracoesPartes.JsonConteudo,
                        MatrimoniosDocumentos = x.ProcuracoesPartes.MatrimoniosDocumentos,
                        PessoasNavigation = x.ProcuracoesPartes.PessoasNavigation,
                        ProcuracoesPartesEstadosNavigation = x.ProcuracoesPartes.ProcuracoesPartesEstadosNavigation,
                        ProcuracoesPartesEstadosPcNavigation = x.ProcuracoesPartes.ProcuracoesPartesEstadosPcNavigation,
                        SolicitacoesNavigation = x.ProcuracoesPartes.SolicitacoesNavigation,
                        TiposProcuracoesPartesNavigation = x.ProcuracoesPartes.TiposProcuracoesPartesNavigation,
                        UsuarioNavigation = new Usuarios
                        {
                            NomeUsuario = x.Usuarios.NomeUsuario,
                            IdUsuario = x.Usuarios.IdUsuario,
                            Email = x.Usuarios.Email
                        }
                    })
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public IEnumerable<ProcuracoesPartes> BuscarPorIdSolicitacaoByJob(long idSolicitacao)
        {
            try
            {
                return _context.ProcuracoesPartes
                    .AsNoTracking()
                    .Include(p => p.PessoasNavigation)
                        .ThenInclude(p => p.PessoasFisicas)
                    .Include(p => p.PessoasNavigation)
                        .ThenInclude(p => p.PessoasContatos)
                    .Include(p => p.PessoasNavigation)
                        .ThenInclude(p => p.PessoasContatos)
                            .ThenInclude(p => p.IdContatoNavigation)
                    //.Include(p => p.MatrimoniosDocumentos)
                    //.Include(p => p.SolicitacoesNavigation)
                    //    .ThenInclude(p => p.Matrimonios)
                    //        .ThenInclude(p => p.MatrimoniosDocumentos)
                    .Join(_context.Usuarios,
                          p => p.IdPessoa,
                          u => u.IdPessoa,
                          (p, u) => new { ProcuracoesPartes = p, Usuarios = u })
                    .Where(x => x.ProcuracoesPartes.IdSolicitacao == idSolicitacao)
                    .Select(x => new ProcuracoesPartes
                    {
                        Email = x.ProcuracoesPartes.Email,
                        EnderecoEntrega = x.ProcuracoesPartes.EnderecoEntrega,
                        IdPessoa = x.ProcuracoesPartes.IdPessoa,
                        DataOperacao = x.ProcuracoesPartes.DataOperacao,
                        IdProcuracaoParte = x.ProcuracoesPartes.IdProcuracaoParte,
                        IdProcuracaoParteEstado = x.ProcuracoesPartes.IdProcuracaoParteEstado,
                        IdSolicitacao = x.ProcuracoesPartes.IdSolicitacao,
                        IdTipoProcuracaoParte = x.ProcuracoesPartes.IdTipoProcuracaoParte,
                        JsonConteudo = x.ProcuracoesPartes.JsonConteudo,
                        MatrimoniosDocumentos = x.ProcuracoesPartes.MatrimoniosDocumentos,
                        PessoasNavigation = x.ProcuracoesPartes.PessoasNavigation,
                        ProcuracoesPartesEstadosNavigation = x.ProcuracoesPartes.ProcuracoesPartesEstadosNavigation,
                        ProcuracoesPartesEstadosPcNavigation = x.ProcuracoesPartes.ProcuracoesPartesEstadosPcNavigation,
                        SolicitacoesNavigation = x.ProcuracoesPartes.SolicitacoesNavigation,
                        TiposProcuracoesPartesNavigation = x.ProcuracoesPartes.TiposProcuracoesPartesNavigation,
                        UsuarioNavigation = new Usuarios
                        {
                            NomeUsuario = x.Usuarios.NomeUsuario,
                            IdUsuario = x.Usuarios.IdUsuario,
                            Email = x.Usuarios.Email
                        }
                    })
                    .ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Participantes>> ObterParticipantes(long idSolicitacao)
        {
            try
            {
                return await _context.ProcuracoesPartes
                                     .Include(p => p.PessoasNavigation)
                                     .Include(p => p.TiposProcuracoesPartesNavigation)
                                     .Join(_context.Usuarios, pp => pp.IdPessoa, u => u.IdPessoa, (pp, u) => new
                                     {
                                         ProcuracoesPartes = pp,
                                         Usuarios = u
                                     })
                                     .Where(x => x.ProcuracoesPartes.IdSolicitacao == idSolicitacao)
                                    .Select(p => new Participantes
                                    {
                                        Nome = p.Usuarios.NomeUsuario,
                                        TipoParticipante = p.ProcuracoesPartes.TiposProcuracoesPartesNavigation.Descricao
                                    })
                                    .OrderByDescending(p => p.TipoParticipante)
                                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
