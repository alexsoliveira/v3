using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Infrastructure.SqlServer.Context;

namespace TGS.Cartorio.Infrastructure.SqlServer.Repositories
{
    public class SolicitacoesNotificacoesSqlRepository : ISolicitacoesNotificacoesSqlRepository
    {
        private readonly EFContext _context;
        private readonly int _tamanhoPagina;

        public SolicitacoesNotificacoesSqlRepository(EFContext context, IConfiguration configuration)
        {
            _context = context;     
            _tamanhoPagina = configuration.GetValue("SqlServer:TamanhoPagina", 50);
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task Incluir(SolicitacoesNotificacoes solicitacaonotificacao)
        {
            _context.SolicitacoesNotificacoes.Add(solicitacaonotificacao);
            await _context.Commit();
        }
        public async Task Atualizar(SolicitacoesNotificacoes solicitacaonotificacao)
        {
            _context.SolicitacoesNotificacoes.Update(solicitacaonotificacao);
            await _context.Commit();
        }

        public async Task<List<SolicitacoesNotificacoes>> BuscarTodos(Expression<Func<SolicitacoesNotificacoes, bool>> func, int pagina = 0)
        {
            return await _context.SolicitacoesNotificacoes
                            .Where(func)
                            .Skip(pagina * _tamanhoPagina)
                            .Take(_tamanhoPagina)
                            .ToListAsync();
        }

        public async Task<List<SolicitacoesNotificacoes>> BuscarTodosComNoLock(Expression<Func<SolicitacoesNotificacoes, bool>> func, int pagina = 0)
        {
            return await _context.SolicitacoesNotificacoes
                   .AsNoTracking()
                   .Where(func)
                   .Skip(pagina * _tamanhoPagina)
                   .Take(_tamanhoPagina)
                   .ToListAsync();
        }

        public void Dispose()
        {
            //_context?.Dispose();
        }  

    }
}