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
    public class TiposFretesPCSqlRepository : ITiposFretesPCSqlRepository
    {
        private readonly EFContext _context;
        private readonly int _tamanhoPagina;

        public TiposFretesPCSqlRepository(EFContext context, IConfiguration configuration)
        {
            _context = context;     
            _tamanhoPagina = configuration.GetValue("SqlServer:TamanhoPagina", 50);
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<List<TiposFretesPc>> BuscarTodos(Expression<Func<TiposFretesPc, bool>> func, int pagina = 0)
        {
            return await _context.TiposFretesPc
                            .Where(func)
                            .Skip(pagina * _tamanhoPagina)
                            .Take(_tamanhoPagina)
                            .ToListAsync();
        }

        public async Task<List<TiposFretesPc>> BuscarTodosComNoLock(Expression<Func<TiposFretesPc, bool>> func, int pagina = 0)
        {
            return await _context.TiposFretesPc
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