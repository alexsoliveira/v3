using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Domain.Services
{

    public class SolicitacoesTaxasService : ISolicitacoesTaxasService
    {
        private readonly ISolicitacoesTaxasSqlRepository _solicitacoesTaxasSqlRepository;
        public SolicitacoesTaxasService(ISolicitacoesTaxasSqlRepository solicitacoesTaxasSqlRepository)
        {
            _solicitacoesTaxasSqlRepository = solicitacoesTaxasSqlRepository;
        }

        public async Task<ICollection<SolicitacoesTaxas>> BuscarPorSolicitacao(long idSolicitacao)
        {
            try
            {
                return await _solicitacoesTaxasSqlRepository.BuscarPorSolicitacao(idSolicitacao);
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
                await _solicitacoesTaxasSqlRepository.Incluir(solicitacoesTaxas);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
