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
    public class SolicitacoesEstadosSqlRepository : ISolicitacoesEstadosSqlRepository
    {
        private readonly EFContext _context;
        private readonly int _tamanhoPagina;

        public SolicitacoesEstadosSqlRepository(EFContext context, IConfiguration configuration)
        {
            _context = context;
            _tamanhoPagina = configuration.GetValue("SqlServer:TamanhoPagina", 50);
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<List<SolicitacoesEstados>> BuscarTodos(Expression<Func<SolicitacoesEstados, bool>> func, int pagina = 0)
        {
            return await _context.SolicitacoesEstados
                            .Where(func)
                            .Skip(pagina * _tamanhoPagina)
                            .Take(_tamanhoPagina)
                            .ToListAsync();
        }

        public async Task<List<SolicitacoesEstados>> BuscarPorSolicitacao(long idSolicitacao)
        {
            return await _context.SolicitacoesEstados
                            .Where(se => se.IdSolicitacao == idSolicitacao)
                            .ToListAsync();
        }

        public async Task<List<SolicitacoesEstados>> BuscarTodosComNoLock(Expression<Func<SolicitacoesEstados, bool>> func, int pagina = 0)
        {
            return await _context.SolicitacoesEstados
                   .AsNoTracking()
                   .Where(func)
                   .Skip(pagina * _tamanhoPagina)
                   .Take(_tamanhoPagina)
                   .ToListAsync();
        }

        public void Dispose()
        {
            ////_context?.Dispose();
        }

        public async Task Incluir(SolicitacoesEstados solicitacoesestados)
        {
            solicitacoesestados.DataOperacao = DateTime.Now;
            _context.SolicitacoesEstados.Add(solicitacoesestados);
            await _context.SaveChangesAsync();
            //await _context.Commit();
        }

        public async Task<SolicitacoesEstados> BuscarId(long id)
        {
            return await _context.SolicitacoesEstados
                .Where(p => p.IdSolicitacaoEstado == id)
                .FirstOrDefaultAsync();
        }
    }
}
