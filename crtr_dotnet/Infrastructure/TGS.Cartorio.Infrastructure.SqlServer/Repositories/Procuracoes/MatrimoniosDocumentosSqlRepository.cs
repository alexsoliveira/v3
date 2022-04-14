using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities.Procuracoes;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer.Procuracoes;
using TGS.Cartorio.Infrastructure.SqlServer.Context;

namespace TGS.Cartorio.Infrastructure.SqlServer.Repositories.Procuracoes
{
    public class MatrimoniosDocumentosSqlRepository : IMatrimoniosDocumentosSqlRepository
    {
        private readonly EFContext _context;

        public MatrimoniosDocumentosSqlRepository(
            EFContext context, 
            IConfiguration configuration)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<MatrimoniosDocumentos>> BuscarPorMatrimonio(long idMatrimonio)
        {
            try
            {
                return await _context.MatrimoniosDocumentos.Where(x => x.IdMatrimonio == idMatrimonio).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<MatrimoniosDocumentos> BuscarPorMatrimonioByJob(long idMatrimonio)
        {
            try
            {
                return _context.MatrimoniosDocumentos
                               .AsNoTracking()
                               .Where(x => x.IdMatrimonio == idMatrimonio)
                               .ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<MatrimoniosDocumentos> BuscarPorMatrimonioComProcuracaoParte(long idMatrimonio, long idPessoaSolicitante)
        {
            try
            {
                var matrimonio = await _context.Matrimonios.FindAsync(idMatrimonio);
                if (matrimonio == null)
                    throw new Exception("Não foi possível localizar o matrimônio!");

                var procuracaoParte = await _context.ProcuracoesPartes.FirstOrDefaultAsync(x => x.IdSolicitacao == matrimonio.IdSolicitacao 
                                                                                             && x.IdPessoa == idPessoaSolicitante);
                if (procuracaoParte == null)
                    throw new Exception("Não foi possível localizar o representante da Solicitação!");

                var matrimonioDocumento = await _context.MatrimoniosDocumentos.FirstOrDefaultAsync(x => x.IdMatrimonio == idMatrimonio 
                                                                            && x.IdProcuracaoParte == procuracaoParte.IdProcuracaoParte);

                return matrimonioDocumento;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<MatrimoniosDocumentos> BuscarPorId(long idMatrimonioDocumento)
        {
            try
            {
                return await _context.MatrimoniosDocumentos.FindAsync(idMatrimonioDocumento);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Atualizar(MatrimoniosDocumentos matrimoniosDocumentos)
        {
            try
            {
                _context.MatrimoniosDocumentos.Update(matrimoniosDocumentos).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ICollection<MatrimoniosDocumentos>> BuscarPorSolicitacao(long idSolicitacao)
        {
            try
            {
                return (await _context.Matrimonios
                             .Include(x => x.MatrimoniosDocumentos)
                             .FirstOrDefaultAsync(x => x.IdSolicitacao == idSolicitacao))?.MatrimoniosDocumentos;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Incluir(MatrimoniosDocumentos matrimoniosDocumentos)
        {
            try
            {
                _context.Add(matrimoniosDocumentos);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Remover(MatrimoniosDocumentos matrimoniosDocumentos)
        {
            try
            {
                _context.Remove(matrimoniosDocumentos);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
