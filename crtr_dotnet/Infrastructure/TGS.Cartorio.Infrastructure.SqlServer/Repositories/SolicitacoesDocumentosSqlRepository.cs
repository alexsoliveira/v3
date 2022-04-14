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
    public class SolicitacoesDocumentosSqlRepository : ISolicitacoesDocumentosSqlRepository
    {
        private readonly EFContext _context;
        private readonly int _tamanhoPagina;

        public SolicitacoesDocumentosSqlRepository(EFContext context, IConfiguration configuration)
        {
            _context = context;     
            _tamanhoPagina = configuration.GetValue("SqlServer:TamanhoPagina", 50);
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task Incluir(SolicitacoesDocumentos solicitacaodocumento)
        {
            _context.SolicitacoesDocumentos.Add(solicitacaodocumento);
            await _context.Commit();
        }
        public async Task Atualizar(SolicitacoesDocumentos solicitacaodocumento)
        {
            _context.SolicitacoesDocumentos.Update(solicitacaodocumento);
            await _context.Commit();
        }

        public async Task<List<SolicitacoesDocumentos>> BuscarTodos(Expression<Func<SolicitacoesDocumentos, bool>> func, int pagina = 0)
        {
            return await _context.SolicitacoesDocumentos
                            .Where(func)
                            .Skip(pagina * _tamanhoPagina)
                            .Take(_tamanhoPagina)
                            .ToListAsync();
        }

        public async Task<List<SolicitacoesDocumentos>> BuscarTodosComNoLock(Expression<Func<SolicitacoesDocumentos, bool>> func, int pagina = 0)
        {
            return await _context.SolicitacoesDocumentos
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

        public async Task<SolicitacoesDocumentos> BuscarId(long id)
        {
            return await _context.SolicitacoesDocumentos
                .Where(p => p.IdSolicitacaoParte == id)
                .FirstOrDefaultAsync();
        }

        public async Task DeletarId(long id)
        {
            _context.SolicitacoesDocumentos.Remove(_context.SolicitacoesDocumentos.Where(p => p.IdSolicitacaoParte == id).FirstOrDefault());
            await _context.Commit();
        }
    }
}