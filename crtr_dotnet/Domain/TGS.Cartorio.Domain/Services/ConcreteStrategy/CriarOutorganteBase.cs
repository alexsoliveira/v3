using AutoMapper;
using System;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities.Procuracoes;
using TGS.Cartorio.Domain.Enumerables;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer.Procuracoes;

namespace TGS.Cartorio.Domain.Services.ConcreteStrategy
{
    public abstract class CriarOutorganteBase
    {
        private readonly IProcuracoesPartesSqlRepository _procuracoesPartesSqlRepository;
        private readonly IProcuracoesPartesEstadosSqlRepository _procuracoesPartesEstadosSqlRepository;
        protected readonly IMapper _mapper;

        public CriarOutorganteBase(IProcuracoesPartesSqlRepository procuracoesPartesSqlRepository,
                                   IProcuracoesPartesEstadosSqlRepository procuracoesPartesEstadosSqlRepository,
                                   IMapper mapper)
        {
            this._procuracoesPartesSqlRepository = procuracoesPartesSqlRepository;
            _procuracoesPartesEstadosSqlRepository = procuracoesPartesEstadosSqlRepository;
            _mapper = mapper;
        }

        public async Task IncluirProcuracaoParte(ProcuracoesPartes data)
        {
            try
            {
                await this._procuracoesPartesSqlRepository.Incluir(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }                       
        }

        protected async Task IncluirProcuracaoParteEstadoInicial(long idProcuracaoParte)
        {
            try
            {
                await _procuracoesPartesEstadosSqlRepository.Incluir(new ProcuracoesPartesEstados
                {
                    IdProcuracaoParte = idProcuracaoParte,
                    IdProcuracaoParteEstadoPc = (int)EProcuracaoParteEstado.Cadastrado,
                    DataOperacao = DateTime.Now
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected ProcuracoesPartes SetProcuracoesPartes(Outorgante data)
        {
            try
            {
                var procuracaoParte = _mapper.Map<ProcuracoesPartes>(data);
                procuracaoParte.IdProcuracaoParteEstado = (int)EProcuracaoParteEstado.Cadastrado;
                procuracaoParte.IdTipoProcuracaoParte = (int)ETipoProcuracaoParte.Outogante;
                procuracaoParte.IdPessoa = data.IdPessoa.HasValue ? data.IdPessoa.Value : 0;
                procuracaoParte.IdUsuario = data.IdUsuario;
                procuracaoParte.IdSolicitacao = data.IdSolicitacao;
                procuracaoParte.DataOperacao = DateTime.Now;
                procuracaoParte.Email = data.Email;
                return procuracaoParte;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
