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
    public class TiposTaxasPcSqlRepository : ITiposTaxasPcSqlRepository
    {
        private readonly EFContext _context;

        public TiposTaxasPcSqlRepository(EFContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<List<TiposTaxasPc>> BuscarTodos()
        {
            try
            {
                return await _context.TiposTaxasPc.ToListAsync();
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

        public async Task<TiposTaxasPc> Find(int id)
        {
            try
            {
                return await _context.TiposTaxasPc.FindAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}