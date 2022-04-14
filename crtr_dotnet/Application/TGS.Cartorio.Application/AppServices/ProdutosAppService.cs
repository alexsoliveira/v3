using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Application.DTO;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Application.AppServices
{
    public class ProdutosAppService : IProdutosAppService
    {
        private readonly IProdutosImagensService _produtoImagensService;
        private readonly IMapper _mapper;
        private readonly IProdutosService _produtoService;
        private readonly ILogSistemaAppService _logSistemaAppService;
        public ProdutosAppService(IProdutosService produtoService,
            IProdutosImagensService produtoImagensService,
            IMapper mapper, 
            ILogSistemaAppService logSistemaAppService)
        {
            _produtoService = produtoService;
            _produtoImagensService = produtoImagensService;
            _mapper = mapper;
            _logSistemaAppService = logSistemaAppService;
        }
        public async Task Incluir(Produtos produto)
        {
            await _produtoService.Incluir(produto);
        }

        public async Task Atualizar(Produtos produto)
        {
            await _produtoService.Atualizar(produto);
        }

        public async Task<List<ProdutosDto>> BuscarTodos(int pagina)
        {
            return _mapper.Map<List<ProdutosDto>>(await _produtoService.BuscarTodos(pagina));
        }

        public async Task<IEnumerable<ProdutosVitrineDto>> BuscarDadosVitrine()
        {
            List<ProdutosVitrineDto> listaProdutosVitrine = new List<ProdutosVitrineDto>();
            List<ProdutosCategoriasPc> produtosCategoria = null;
            try
            {
                produtosCategoria = await _produtoService.BuscarDadosVitrine();
                if (produtosCategoria == null || produtosCategoria.Count == 0)
                    await _logSistemaAppService.Add(CodLogSistema.Erro_ProdutosAppService_BuscarDadosVitrine_ProdutosCategoriasPcNaoLocalizados,
                    new
                    {
                        MsgErro = "Não foi possível buscar os dados dos ProdutosCategoriasPc!"
                    });
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_ProdutosAppService_BuscarDadosVitrine,
                    new
                    {
                        MsgErro = "Não foi possível buscar os dados da vitrine!"
                    }, ex);

                throw;
            }
            

            foreach (var categoria in produtosCategoria)
            {
                try
                {
                    ProdutosVitrineDto produtosVitrine = new ProdutosVitrineDto
                    {
                        IdProdutoCategoria = categoria.IdProdutoCategoria,
                        Titulo = categoria.Titulo,
                        Descricao = categoria.Descricao,
                        Produtos = categoria.Produtos.ToList()
                    };

                    listaProdutosVitrine.Add(produtosVitrine);
                }
                catch (Exception ex)
                {
                    await _logSistemaAppService.Add(CodLogSistema.Erro_ProdutosAppService_BuscarDadosVitrine_ListaProdutosVitrine,
                    new
                    {
                        MsgErro = "Ocorreu um erro ao adicionar item na lista listaProdutosVitrine"
                    }, ex);
                    
                    continue;
                }
            }

            return listaProdutosVitrine;
        }

        public async Task<DetalhesProdutoDto> BuscarDetalhesProduto(int id)
        {
            Produtos produto = null;
            try
            {
                produto = await _produtoService.BuscarDetalhesProduto(id);
                if (produto == null)
                    return null;
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_ProdutosAppService_BuscarDetalhesProduto,
                    new
                    {
                        IdProduto = id
                    }, ex);

                throw;
            }
            

            var detalhesProduto = new DetalhesProdutoDto
            {
                IdProduto = produto.IdProduto,
                Campos = produto.Campos,
                Descricao = produto.Descricao,
                FlagAtivo = produto.FlagAtivo,
                Titulo = produto.Titulo,
            };

            if (produto.IdProdutoCategoriaNavigation != null)
                detalhesProduto.TituloCategoriaProduto = produto.IdProdutoCategoriaNavigation.Titulo;

            try
            {
                var produtosImagens = _mapper.Map<ICollection<ProdutosImagemDto>>(produto.ProdutosImagens);
                if (produtosImagens != null)
                    detalhesProduto.ProdutosImagens = produtosImagens;
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_ProdutosAppService_BuscarDetalhesProduto_DesserializarProdutosImagens,
                    new
                    {
                        IdProduto = id,
                        ProdutosImagens = produto.ProdutosImagens
                    }, ex);

                throw;
            }
            

            detalhesProduto.ProdutosModalidades = new List<ProdutosModalidadesDto>();

            foreach (var produtoModalidade in produto.ProdutosModalidades)
            {
                try
                {
                    detalhesProduto.ProdutosModalidades.Add(new ProdutosModalidadesDto
                    {
                        BlobConteudo = produtoModalidade.IdModalidadeNavigation.BlobConteudo,
                        StrBlobConteudo = produtoModalidade.IdModalidadeNavigation.StrBlobConteudo,
                        Conteudo = produtoModalidade.Conteudo,
                        Descricao = produtoModalidade.IdModalidadeNavigation.Descricao,
                        IdModalidade = produtoModalidade.IdModalidade,
                        Titulo = produtoModalidade.IdModalidadeNavigation.Titulo
                    });
                }
                catch (Exception ex)
                {
                    await _logSistemaAppService.Add(CodLogSistema.Erro_ProdutosAppService_BuscarDetalhesProduto_AddProdutosModalidades,
                    new
                    {
                        IdProduto = id,
                        ProdutosModalidades = produtoModalidade
                    }, ex);

                    throw;
                }
            }

            return detalhesProduto;
        }

        public async Task<List<Produtos>> BuscarTodosComNoLock(int pagina)
        {
            return await _produtoService.BuscarTodosComNoLock(pagina);
        }
        public async Task<List<Produtos>> BuscarTodosComNoLock(Expression<Func<Produtos, bool>> func)
        {
            return await _produtoService.BuscarTodosComNoLock(func);
        }
        public async Task<ProdutosDto> BuscarId(int id)
        {
            Produtos produto = null;
            try
            {
                produto = await _produtoService.BuscarId(id);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_ProdutosAppService_BuscarId,
                    new
                    {
                        IdProduto = id
                    }, ex);

                throw;
            }

            ProdutosDto produtoDto = null;
            try
            {
                produtoDto = _mapper.Map<ProdutosDto>(produto);

                if (produtoDto == null)
                    throw new Exception("Produto não encontrado.");
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_ProdutosAppService_BuscarId_DesserializarParaDto,
                    new
                    {
                        Produto = produto,
                        IdProduto = id
                    }, ex);
                throw;
            }

            return produtoDto;
        }
    }
}