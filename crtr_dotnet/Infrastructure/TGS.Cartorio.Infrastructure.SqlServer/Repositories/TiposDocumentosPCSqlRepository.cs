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
    public class TiposDocumentosPCSqlRepository : ITiposDocumentosPCSqlRepository
    {
        private readonly EFContext _context;
        private readonly int _tamanhoPagina;

        public TiposDocumentosPCSqlRepository(EFContext context, IConfiguration configuration)
        {
            _context = context;     
            _tamanhoPagina = configuration.GetValue("SqlServer:TamanhoPagina", 50);
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<List<TiposDocumentosPc>> BuscarTodos(Expression<Func<TiposDocumentosPc, bool>> func, int pagina = 0)
        {
            return await _context.TiposDocumentosPc
                            .Where(func)
                            .Skip(pagina * _tamanhoPagina)
                            .Take(_tamanhoPagina)
                            .ToListAsync();
        }

        public async Task<List<TiposDocumentosPc>> BuscarTodosComNoLock(Expression<Func<TiposDocumentosPc, bool>> func, int pagina = 0)
        {
            return await _context.TiposDocumentosPc
                   .AsNoTracking()
                   .Where(func)
                   .Skip(pagina * _tamanhoPagina)
                   .Take(_tamanhoPagina)
                   .ToListAsync();
        }

        public async Task<TiposDocumentosPc> BuscarId(int IdTipoDocumento)
        {
            return await _context.TiposDocumentosPc
                .Where(p => p.IdTipoDocumento == IdTipoDocumento)
                .FirstOrDefaultAsync();
        }
        

        public void Dispose()
        {
            //_context?.Dispose();
        }  

    }
}