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
    public class ProdutosModalidadesPCSqlRepository : IProdutosModalidadesPCSqlRepository
    {
        private readonly EFContext _context;
        private readonly int _tamanhoPagina;

        public ProdutosModalidadesPCSqlRepository(EFContext context, IConfiguration configuration)
        {
            _context = context;     
            _tamanhoPagina = configuration.GetValue("SqlServer:TamanhoPagina", 50);
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<List<ProdutosModalidadesPc>> BuscarTodos(Expression<Func<ProdutosModalidadesPc, bool>> func, int pagina = 0)
        {
            return await _context.ProdutosModalidadesPc
                            .Where(func)
                            .Skip(pagina * _tamanhoPagina)
                            .Take(_tamanhoPagina)
                            .ToListAsync();
        }

        public async Task<List<ProdutosModalidadesPc>> BuscarTodos()
        {
            return await _context.ProdutosModalidadesPc.ToListAsync();
        }

        public async Task<List<ProdutosModalidadesPc>> BuscarTodosComNoLock(Expression<Func<ProdutosModalidadesPc, bool>> func, int pagina = 0)
        {
            return await _context.ProdutosModalidadesPc
                   .AsNoTracking()
                   .Where(func)
                   .Skip(pagina * _tamanhoPagina)
                   .Take(_tamanhoPagina)
                   .ToListAsync();
        }

        public async Task<ProdutosModalidadesPc> BuscarId(int id)
        {
            return await _context.ProdutosModalidadesPc
                   .Where(x => x.IdModalidade == id)
                   .FirstOrDefaultAsync();
        }

        public void Dispose()
        {
            //_context?.Dispose();
        }  

    }
}