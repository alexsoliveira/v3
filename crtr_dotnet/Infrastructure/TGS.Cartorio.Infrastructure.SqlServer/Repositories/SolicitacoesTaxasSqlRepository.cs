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
    public class SolicitacoesTaxasSqlRepository : ISolicitacoesTaxasSqlRepository
    {
        private readonly EFContext _context;

        public SolicitacoesTaxasSqlRepository(EFContext context, IConfiguration configuration)
        {
            _context = context;     
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<ICollection<SolicitacoesTaxas>> BuscarPorSolicitacao(long idSolicitacao)
        {
            try
            {
                return await _context.SolicitacoesTaxas
                    .Include(s => s.CartoriosTaxasNavigation)
                    .Include(s => s.TaxasExtrasNavigation)
                    .Where(p => p.IdSolicitacao == idSolicitacao).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Dispose()
        {
            try
            {
                //_context?.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Incluir(SolicitacoesTaxas solicitacoesTaxas)
        {
            try
            {
                _context.SolicitacoesTaxas.Add(solicitacoesTaxas);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}