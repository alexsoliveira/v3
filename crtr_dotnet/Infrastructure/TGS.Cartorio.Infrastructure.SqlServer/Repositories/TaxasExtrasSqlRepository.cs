using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Infrastructure.SqlServer.Context;

namespace TGS.Cartorio.Infrastructure.SqlServer.Repositories
{
    public class TaxasExtrasSqlRepository : ITaxasExtrasSqlRepository
    {
        private readonly EFContext _context;

        public TaxasExtrasSqlRepository(EFContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<List<TaxasExtras>> BuscarTodos()
        {
            try
            {
                return await _context.TaxasExtras
                    .Include(p => p.TiposTaxasPcNavigation)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Dispose()
        {
            //_context?.Dispose();
        }

        public async Task<TaxasExtras> Find(long id)
        {
            try
            {
                return await _context.TaxasExtras
                    .Include(p => p.TiposTaxasPcNavigation)
                    .FirstOrDefaultAsync(p => p.IdTaxaExtra == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<TaxasExtras>> Pesquisar(Expression<Func<TaxasExtras, bool>> func)
        {
            try
            {
                return await _context.TaxasExtras
                    .Include(p => p.TiposTaxasPcNavigation)
                    .Where(func)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}