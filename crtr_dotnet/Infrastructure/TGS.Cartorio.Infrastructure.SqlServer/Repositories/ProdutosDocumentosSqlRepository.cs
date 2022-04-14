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
    public class ProdutosDocumentosSqlRepository : IProdutosDocumentosSqlRepository
    {
        private readonly EFContext _context;
        private readonly int _tamanhoPagina;
        public ProdutosDocumentosSqlRepository(EFContext context, IConfiguration configuration)
        {
            _context = context;
            _tamanhoPagina = configuration.GetValue("SqlServer:TamanhoPagina", 50);
        }

        public IUnitOfWork UnitOfWork => throw new NotImplementedException();
        public async Task Incluir(ProdutosDocumentos produtosdocumentos)
        {
            _context.ProdutosDocumentos.Add(produtosdocumentos);
            await _context.Commit();
        }
        public async Task Atualizar(ProdutosDocumentos produtosdocumentos)
        {
            _context.ProdutosDocumentos.Update(produtosdocumentos);
            await _context.Commit();
        }

        public async Task<List<ProdutosDocumentos>> BuscarTodos(Expression<Func<ProdutosDocumentos, bool>> func, int pagina = 0)
        {
            return await _context.ProdutosDocumentos
                            .Where(func)
                            .Skip(pagina * _tamanhoPagina)
                            .Take(_tamanhoPagina)
                            .ToListAsync();
        }

        public async Task<List<ProdutosDocumentos>> BuscarTodosComNoLock(Expression<Func<ProdutosDocumentos, bool>> func, int pagina = 0)
        {
            return await _context.ProdutosDocumentos
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

        public async Task<ProdutosDocumentos> BuscarId(int id)
        {
            return await _context.ProdutosDocumentos
                .Where(p => p.IdProdutoDocumentos == id)
                .FirstOrDefaultAsync();
        }
    }
}
