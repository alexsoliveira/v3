using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities.Procuracoes;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer.Procuracoes;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Domain.Services
{

    public class ProcuracoesPartesEstadosService : IProcuracoesPartesEstadosService
    {
        private readonly IProcuracoesPartesEstadosSqlRepository _procuracoesPartesEstadosRepository;

        public ProcuracoesPartesEstadosService(IProcuracoesPartesEstadosSqlRepository procuracoesPartesEstadosRepository)
        {
            _procuracoesPartesEstadosRepository = procuracoesPartesEstadosRepository;
        }

        public async Task<ProcuracoesPartesEstados> BuscarId(long id)
        {
            try
            {
                return await _procuracoesPartesEstadosRepository.BuscarId(id);
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
                return await _procuracoesPartesEstadosRepository.BuscarPorProcuracaoParte(idProcuracaoParte);
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
                await _procuracoesPartesEstadosRepository.Incluir(procuracoesPartesEstados);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
