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
    public class ProdutosImagensSqlRepository : IProdutosImagensSqlRepository
    {
        private readonly EFContext _context;
        private readonly int _tamanhoPagina;
        public ProdutosImagensSqlRepository(EFContext context, IConfiguration configuration)
        {
            _context = context;
            _tamanhoPagina = configuration.GetValue("SqlServer:TamanhoPagina", 50);
        }

        public IUnitOfWork UnitOfWork => throw new NotImplementedException();
        public async Task Incluir(ProdutosImagens produtoimagem)
        {
            _context.ProdutosImagens.Add(produtoimagem);
            await _context.Commit();
        }
        public async Task Atualizar(ProdutosImagens produtoimagem)
        {
            _context.ProdutosImagens.Update(produtoimagem);
            await _context.Commit();
        }

        public async Task<List<ProdutosImagens>> BuscarTodos(Expression<Func<ProdutosImagens, bool>> func, int pagina = 0)
        {
            return await _context.ProdutosImagens
                            .Where(func)
                            .Skip(pagina * _tamanhoPagina)
                            .Take(_tamanhoPagina)
                            .ToListAsync();
        }

        public async Task<List<ProdutosImagens>> BuscarTodosComNoLock(Expression<Func<ProdutosImagens, bool>> func, int pagina = 0)
        {
            return await _context.ProdutosImagens
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

        public async Task<ProdutosImagens> BuscarId(int id)
        {
            return await _context.ProdutosImagens
                .Where(p => p.IdProdutoImagem == id)
                .FirstOrDefaultAsync();
        }
    }
}
