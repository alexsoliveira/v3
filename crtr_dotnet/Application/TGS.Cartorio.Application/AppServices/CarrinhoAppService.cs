using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Application.DTO;
using TGS.Cartorio.Application.DTO.Products.Matrimonio;
using TGS.Cartorio.Application.Enumerables;
using TGS.Cartorio.Application.ViewModel;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Application.AppServices
{
    public class CarrinhoAppService : ICarrinhoAppService
    {
        private readonly ISolicitacoesAppService _solicitacoesAppService;
        private readonly IProcuracoesPartesService _procuracoesPartesService;
        private readonly IPessoasAppService _pessoaAppService;
        private readonly IUsuariosAppService _usuarioAppService;
        private readonly IConfiguracoesAppService _configuracoesAppService;
        private readonly IMapper _mapper;
        private readonly IProdutosAppService _produtosAppService;

        public CarrinhoAppService(
            ISolicitacoesAppService solicitacoesAppService,
            IPessoasAppService pessoaAppService,
            IUsuariosAppService usuarioAppService,
            IConfiguracoesAppService configuracoesAppService,
            IMapper mapper,
            IProdutosAppService produtosAppService,
            IProcuracoesPartesService procuracoesPartesService)
        {
            this._solicitacoesAppService = solicitacoesAppService;
            this._pessoaAppService = pessoaAppService;
            this._usuarioAppService = usuarioAppService;
            this._configuracoesAppService = configuracoesAppService;
            this._mapper = mapper;
            this._produtosAppService = produtosAppService;
            _procuracoesPartesService = procuracoesPartesService;
        }

        public async Task<Usuarios> BuscarSolicitante(long id)
        {
            try
            {
                var solicitacao = await _solicitacoesAppService.BuscarId(id);
                var pessoa = await _pessoaAppService.BuscarId(solicitacao.IdPessoa.Value);
                var usuario = await _usuarioAppService.BuscarId(pessoa.IdUsuario);

                return usuario;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public async Task<ProdutosDto> ObterProduto(long id)
        {
            try
            {
                var solicitacao = await _solicitacoesAppService.BuscarId(id);
                var produto = await _produtosAppService.BuscarId(solicitacao.IdProduto);
                return produto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ComposicaoPrecoDTO>> ObterComposicaoPrecos(long id)
        {
            try
            {
                List<ComposicaoPrecoDTO> listComposicaoPreco = new List<ComposicaoPrecoDTO>();
                var composicaoPreco = new ComposicaoPrecoDTO();
                var solicitacao = await _solicitacoesAppService.BuscarId(id);
                var produto = await _produtosAppService.BuscarId(solicitacao.IdProduto);

                if (!string.IsNullOrEmpty(produto.Campos))
                {
                    var campos = JsonConvert.DeserializeObject<ComposicaoPrecoDTO>(produto.Campos);
                    composicaoPreco.Disponivel = campos.Disponivel;
                    composicaoPreco.Servico = campos.Servico;
                    composicaoPreco.Observacao = campos.Observacao;
                    composicaoPreco.Destino = campos.Destino;
                    composicaoPreco.Custo = campos.Custo;
                    listComposicaoPreco.Add(composicaoPreco);
                }

                return listComposicaoPreco;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TermoConcordanciaDTO> ObterTermoConcordancia(string descricao)
        {
            try
            {
                var config = await _configuracoesAppService.BuscarPorDescricao(p => p.Descricao == descricao);                
                var termo = _mapper.Map<TermoConcordanciaDTO>(config);

                return termo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<ParticipantesDto>> ObterParticipantes(long idSolicitacao)
        {
            try
            {
                var participantes = await _procuracoesPartesService.ObterParticipantes(idSolicitacao);

                return _mapper.Map<IEnumerable<ParticipantesDto>>(participantes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AceiteTermoConcordancia(long idSolicitacao,  bool isTermoAceito)
        {
            try
            {
                var solicitacao = await _solicitacoesAppService.BuscarId(idSolicitacao);
                if(solicitacao.Conteudo == null)
                {
                    solicitacao.Conteudo = JsonConvert.SerializeObject(new SolicitacaoConteudoViewModel());
                }
                var conteudoSolicitacao = JsonConvert.DeserializeObject<SolicitacaoConteudoViewModel>(solicitacao.Conteudo);

                if (conteudoSolicitacao == null)
                    conteudoSolicitacao = new SolicitacaoConteudoViewModel();

                conteudoSolicitacao.DataTermoDeAceite = DateTime.Now;
                conteudoSolicitacao.AceitouTermoDeAceite = isTermoAceito;

                solicitacao.Conteudo = JsonConvert.SerializeObject(conteudoSolicitacao);

                solicitacao.IdSolicitacaoEstado = GerenciadorEstadosSolicitacao.ProximoEstadoSolicitacao(solicitacao.IdSolicitacaoEstado, TelasSolicitacao.Carrinho);

                await _solicitacoesAppService.Atualizar(_mapper.Map<SolicitacoesDto>(solicitacao));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
