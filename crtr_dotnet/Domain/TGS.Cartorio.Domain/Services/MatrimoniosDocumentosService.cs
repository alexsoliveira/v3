using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities.Procuracoes;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer.Procuracoes;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Domain.Services
{
    public class MatrimoniosDocumentosService : IMatrimoniosDocumentosService
    {
        private readonly IMatrimoniosDocumentosSqlRepository _matrimoniosDocumentosSqlRepository;
        public MatrimoniosDocumentosService(IMatrimoniosDocumentosSqlRepository matrimoniosDocumentosSqlRepository)
        {
            _matrimoniosDocumentosSqlRepository = matrimoniosDocumentosSqlRepository;
        }

        public async Task<IEnumerable<MatrimoniosDocumentos>> BuscarPorMatrimonio(long idMatrimonio)
        {
            try
            {
                return await _matrimoniosDocumentosSqlRepository.BuscarPorMatrimonio(idMatrimonio);
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
                return _matrimoniosDocumentosSqlRepository.BuscarPorMatrimonioByJob(idMatrimonio);
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
                return await _matrimoniosDocumentosSqlRepository.BuscarPorMatrimonioComProcuracaoParte(idMatrimonio, idPessoaSolicitante);
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
                return await _matrimoniosDocumentosSqlRepository.BuscarPorId(idMatrimonioDocumento);
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
                await _matrimoniosDocumentosSqlRepository.Atualizar(matrimoniosDocumentos);
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
                return await _matrimoniosDocumentosSqlRepository.BuscarPorSolicitacao(idSolicitacao);
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
                await _matrimoniosDocumentosSqlRepository.Incluir(matrimoniosDocumentos);
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
                await _matrimoniosDocumentosSqlRepository.Remover(matrimoniosDocumentos);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
