using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities.Procuracoes;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer.Procuracoes;
using TGS.Cartorio.Infrastructure.SqlServer.Context;
using Microsoft.EntityFrameworkCore;

namespace TGS.Cartorio.Infrastructure.SqlServer.Repositories.Procuracoes
{
    public class ProcuracoesPartesEstadosSqlRepository : IProcuracoesPartesEstadosSqlRepository
    {
        private readonly EFContext _context;
        private readonly int _tamanhoPagina;

        public ProcuracoesPartesEstadosSqlRepository(EFContext context, IConfiguration configuration)
        {
            _context = context;
            _tamanhoPagina = configuration.GetValue("SqlServer:TamanhoPagina", 50);
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<ProcuracoesPartesEstados> BuscarId(long id)
        {
            try
            {
                return await _context.ProcuracoesPartesEstados.FindAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ProcuracoesPartesEstados>> BuscarPorProcuracaoParte(long idProcuracaoParte)
        {
            try
            {
                return await _context.ProcuracoesPartesEstados.Where(ppe => ppe.IdProcuracaoParte == idProcuracaoParte).ToListAsync();
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
                ////_context?.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Incluir(ProcuracoesPartesEstados procuracoesPartesEstados)
        {
            try
            {
                _context.ProcuracoesPartesEstados.Add(procuracoesPartesEstados);
                await _context.Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
