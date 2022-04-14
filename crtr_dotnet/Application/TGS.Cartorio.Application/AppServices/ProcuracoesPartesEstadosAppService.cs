using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Application.DTO;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Entities.Procuracoes;
using TGS.Cartorio.Domain.Interfaces.Services;



namespace TGS.Cartorio.Application.AppServices
{
    public class ProcuracoesPartesEstadosAppService : IProcuracoesPartesEstadosAppService
    {
        private readonly IProcuracoesPartesEstadosService _procuracoesPartesEstadosService;
        private readonly IMapper _mapper;

        public ProcuracoesPartesEstadosAppService(IProcuracoesPartesEstadosService procuracoesPartesEstadosService, IMapper mapper)
        {
            _procuracoesPartesEstadosService = procuracoesPartesEstadosService;
            _mapper = mapper;
        }

        public async Task<ProcuracoesPartesEstadosDto> BuscarPorId(long id)
        {
            try
            {
                var procuracaoParteEstado = await _procuracoesPartesEstadosService.BuscarId(id);
                if (procuracaoParteEstado == null)
                    return null;

                return _mapper.Map<ProcuracoesPartesEstadosDto>(procuracaoParteEstado);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ProcuracoesPartesEstadosDto>> BuscarPorProcuracaoParte(long idProcuracaoParte)
        {
            try
            {
                var procuracoesPartesEstados = await _procuracoesPartesEstadosService.BuscarPorProcuracaoParte(idProcuracaoParte);
                if (procuracoesPartesEstados == null || procuracoesPartesEstados.Count() == 0)
                    return null;

                return _mapper.Map<IEnumerable<ProcuracoesPartesEstadosDto>>(procuracoesPartesEstados);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Incluir(ProcuracoesPartesEstadosDto procuracoesPartesEstadosDto)
        {
            try
            {
                var procuracaoParteEstado = _mapper.Map<ProcuracoesPartesEstados>(procuracoesPartesEstadosDto);
                if (procuracaoParteEstado != null)
                    await _procuracoesPartesEstadosService.Incluir(procuracaoParteEstado);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
