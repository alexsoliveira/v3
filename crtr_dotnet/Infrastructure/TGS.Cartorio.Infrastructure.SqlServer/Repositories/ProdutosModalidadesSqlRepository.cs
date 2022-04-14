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
    public class ProdutosModalidadesSqlRepository : IProdutosModalidadesSqlRepository
    {
        private readonly EFContext _context;
        private readonly int _tamanhoPagina;
        public ProdutosModalidadesSqlRepository(EFContext context, IConfiguration configuration)
        {
            _context = context;
            _tamanhoPagina = configuration.GetValue("SqlServer:TamanhoPagina", 50);
        }


        public async Task Incluir(ProdutosModalidades produtosmodalidades)
        {
            _context.ProdutosModalidades.Add(produtosmodalidades);
            await _context.Commit();
        }

        public async Task<List<ProdutosModalidades>> BuscarTodos(Expression<Func<ProdutosModalidades, bool>> func, int pagina = 0)
        {
            return await _context.ProdutosModalidades
                          .Where(func)
                          .Skip(pagina * _tamanhoPagina)
                          .Take(_tamanhoPagina)
                          .ToListAsync();
        }

        public async Task<List<ProdutosModalidades>> BuscarTodosComNoLock(Expression<Func<ProdutosModalidades, bool>> func, int pagina = 0)
        {
            return await _context.ProdutosModalidades
                 .AsNoTracking()
                 .Where(func)
                 .Skip(pagina * _tamanhoPagina)
                 .Take(_tamanhoPagina)
                 .ToListAsync();
        }

        public async Task<ProdutosModalidades> BuscarId(int id)
        {
            return await _context.ProdutosModalidades
                .Where(p => p.IdProdutoModalidade == id)
                .FirstOrDefaultAsync();
        }

        public async Task Atualizar(ProdutosModalidades produtosmodalidades)
        {
            _context.ProdutosModalidades.Update(produtosmodalidades);
            await _context.Commit();
        }
    }
}
