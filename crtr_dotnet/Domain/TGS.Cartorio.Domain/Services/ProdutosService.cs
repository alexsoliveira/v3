using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Domain.Services
{
    
	  public class ProdutosService : IProdutosService
    {		    
        private readonly IProdutosSqlRepository _produtosRepository;

        public ProdutosService(IProdutosSqlRepository produtosRepository)
        {
            _produtosRepository = produtosRepository;
        }

        public async Task Incluir(Produtos produto)
        {
            await _produtosRepository.Incluir(produto);
        }

        public async Task Atualizar(Produtos produto)
        {
            produto.DataOperacao = DateTime.Now;
            await _produtosRepository.Atualizar(produto);
        }

        public async Task<List<ProdutosCategoriasPc>> BuscarDadosVitrine()
        {
            return await _produtosRepository.BuscarDadosVitrine();
        }

        public async Task<Produtos> BuscarDetalhesProduto(int id)
        {
            return await _produtosRepository.BuscarId(id);
        }

        public async Task<List<Produtos>> BuscarTodos(int pagina)
        {
            return await _produtosRepository.BuscarTodos(u => u.FlagAtivo == true, pagina);
        }

        public async Task<List<Produtos>> BuscarTodosComNoLock(int pagina)
        {
            return await _produtosRepository.BuscarTodosComNoLock(u => u.FlagAtivo == true, pagina);
        }
        
        public async  Task<Produtos> BuscarId(int id)
        {
            return await _produtosRepository.BuscarId(id);
        }

        public async Task<List<Produtos>> BuscarTodosComNoLock(Expression<Func<Produtos, bool>> func, int pagina = 0)
        {
            return await _produtosRepository.BuscarTodosComNoLock(func, pagina);
        }
    }
}
