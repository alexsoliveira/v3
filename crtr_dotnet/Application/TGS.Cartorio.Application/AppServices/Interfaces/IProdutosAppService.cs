using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Application.DTO;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Application.AppServices.Interfaces
{
    public interface IProdutosAppService
    {
        Task Incluir(Produtos produto);
        Task Atualizar(Produtos produto);
        Task<List<ProdutosDto>> BuscarTodos(int pagina = 0);
        Task<IEnumerable<ProdutosVitrineDto>> BuscarDadosVitrine();
        Task<DetalhesProdutoDto> BuscarDetalhesProduto(int id);
        Task<List<Produtos>> BuscarTodosComNoLock(int pagina = 0);
        Task<ProdutosDto> BuscarId(int id);
        Task<List<Produtos>> BuscarTodosComNoLock(Expression<Func<Produtos, bool>> func);
    }
}