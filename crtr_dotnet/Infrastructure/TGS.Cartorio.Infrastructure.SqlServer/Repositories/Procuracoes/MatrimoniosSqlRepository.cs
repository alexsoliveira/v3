using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities.Procuracoes;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer.Procuracoes;
using TGS.Cartorio.Infrastructure.SqlServer.Context;

namespace TGS.Cartorio.Infrastructure.SqlServer.Repositories.Procuracoes
{
    public class MatrimoniosSqlRepository : IMatrimoniosSqlRepository
    {
        private readonly EFContext _context;
        private readonly int _tamanhoPagina;

        public MatrimoniosSqlRepository(
            EFContext context, 
            IConfiguration configuration)
        {
            _context = context;
            _tamanhoPagina = configuration.GetValue("SqlServer:TamanhoPagina", 50);
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task Incluir(Matrimonios matrimonios)
        {
            try
            {
                _context.Matrimonios.Add(matrimonios);
                await _context.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Atualizar(Matrimonios matrimonios)
        {
            try
            {
                _context.Attach(matrimonios).State = EntityState.Modified;
                await _context.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Existe(long idMatrimonio)
        {
            try
            {
                return await _context.Matrimonios.AnyAsync(x => x.IdMatrimonio == idMatrimonio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Matrimonios> BuscarPorSolicitacao(long idSolicitacao)
        {
            try
            {
                return await _context.Matrimonios
                                     .AsNoTracking()
                                     .Include(x => x.IdSolicitacaoNavigation)
                                     .Where(m => m.IdSolicitacao == idSolicitacao)
                                     .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Matrimonios BuscarPorSolicitacaoByJob(long idSolicitacao)
        {
            try
            {
                return _context.Matrimonios
                                     .AsNoTracking()
                                     .Include(x => x.IdSolicitacaoNavigation)
                                     .Where(m => m.IdSolicitacao == idSolicitacao)
                                     .FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Matrimonios> BuscarPorId(long idMatrimonio)
        {
            try
            {
                return await _context.Matrimonios.FindAsync(idMatrimonio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
