using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Application.DTO;
using TGS.Cartorio.Domain.Entities.Procuracoes;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer.Procuracoes;
using TGS.Cartorio.Domain.Interfaces.Services;
using TGS.Cartorio.Domain.Services.OutorgadoProcuracoesPartesStrategy;

namespace TGS.Cartorio.Application.AppServices
{
    public class ProcuracoesPartesAppService : IProcuracoesPartesAppService
    {
        private readonly IProcuracoesPartesService _procuracoesPartesService;
        private readonly IMapper _mapper;
        private readonly IProcuracoesPartesSqlRepository _procuracoesPartesSqlRepository;
        private readonly IProcuracoesPartesEstadosSqlRepository _procuracoesPartesEstadosSqlRepository;
        private readonly IUsuariosSqlRepository _usuarioRepository;
        private readonly IPessoasSqlRepository _pessoaSqlRepository;

        public ProcuracoesPartesAppService(IProcuracoesPartesService procuracoesPartesService, 
            IMapper mapper, 
            IProcuracoesPartesSqlRepository procuracoesPartesSqlRepository, 
            IProcuracoesPartesEstadosSqlRepository procuracoesPartesEstadosSqlRepository, 
            IUsuariosSqlRepository usuarioRepository, 
            IPessoasSqlRepository pessoaSqlRepository)
        {
            _procuracoesPartesService = procuracoesPartesService;
            _mapper = mapper;
            _procuracoesPartesSqlRepository = procuracoesPartesSqlRepository;
            _procuracoesPartesEstadosSqlRepository = procuracoesPartesEstadosSqlRepository;
            _usuarioRepository = usuarioRepository;
            _pessoaSqlRepository = pessoaSqlRepository;
        }

        public async Task<ProcuracoesPartesDto> BuscarPorId(long id)
        {
            try
            {
                ProcuracoesPartes procuracoesPartes = await _procuracoesPartesService.BuscarPorId(id);
                if (procuracoesPartes == null)
                    return null;

                return _mapper.Map<ProcuracoesPartesDto>(procuracoesPartes);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task AtualizarOutorgantes(SolicitacoesOutorgantesDto solicitacaoOutorgantes)
        {
            try
            {
                //outorgantes para add
                IEnumerable<Outorgante> outorgantes = await _procuracoesPartesService.AtualizarOutorgantes(_mapper.Map<SolicitacoesOutorgantes>(solicitacaoOutorgantes));
                if (outorgantes == null)
                    return;

                foreach (var outorganteDto in _mapper.Map<IEnumerable<OutorgantesDto>>(outorgantes))
                {
                    await outorganteDto.ValidarRegrasCriacaoOutorgante(
                          solicitacaoOutorgantes.IdPessoaSolicitante,
                          this._procuracoesPartesSqlRepository,
                          this._procuracoesPartesEstadosSqlRepository,
                          this._usuarioRepository,
                          this._pessoaSqlRepository,
                          this._mapper);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }



        public async Task AtualizarOutorgados(SolicitacoesOutorgadosDto solicitacaoOutorgados)
        {
            try
            {
                //outorgados para add
                IEnumerable<Outorgados> outorgados = await _procuracoesPartesService.AtualizarOutorgados(_mapper.Map<SolicitacoesOutorgados>(solicitacaoOutorgados));
                if (outorgados == null || outorgados.Count() == 0)
                    return;

                foreach (var outorgadoDto in _mapper.Map<IEnumerable<OutorgadoDto>>(outorgados))
                {
                    await OutorgadoContext.Resolve(_mapper.Map<Outorgados>(outorgadoDto),
                                                       _mapper,
                                                       _procuracoesPartesSqlRepository,
                                                       _usuarioRepository,
                                                       _pessoaSqlRepository);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ProcuracoesPartesDto>> BuscarPorIdSolicitacao(long idSolicitacao)
        {
            try
            {
                IEnumerable<ProcuracoesPartes> procuracoes = await _procuracoesPartesService.BuscarPorIdSolicitacao(idSolicitacao);
                if (procuracoes == null)
                    return null;

                return _mapper.Map<IEnumerable<ProcuracoesPartesDto>>(procuracoes);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Incluir(ProcuracoesPartesDto procuracoesPartesDto)
        {
            try
            {
                if (procuracoesPartesDto == null)
                    throw new Exception("Objeto ProcuracoesPartesDto está nulo!");

                var procuracoes = _mapper.Map<ProcuracoesPartes>(procuracoesPartesDto);
                await _procuracoesPartesService.Incluir(procuracoes);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
