using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TGS.Cartorio.Domain.Interfaces.Services
{
    public interface  IProdutosService
    {
        Task Incluir(Produtos produto);
        Task Atualizar(Produtos produto);
        Task<List<Produtos>> BuscarTodos(int pagina = 0);
        Task<Produtos> BuscarDetalhesProduto(int id);
        Task<List<ProdutosCategoriasPc>> BuscarDadosVitrine();
        Task<List<Produtos>> BuscarTodosComNoLock(int pagina = 0);
        Task<List<Produtos>> BuscarTodosComNoLock(Expression<Func<Produtos,bool>> func, int pagina = 0);
        Task<Produtos> BuscarId(int id);
    }
}
