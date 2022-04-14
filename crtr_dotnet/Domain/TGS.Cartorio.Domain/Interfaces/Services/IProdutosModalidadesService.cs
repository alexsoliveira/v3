using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Domain.Interfaces.Services
{
    public interface IProdutosModalidadesService
    {
        Task Incluir(ProdutosModalidades produtosmodalidades);
        Task Atualizar(ProdutosModalidades produtosmodalidades);
        Task<List<ProdutosModalidades>> BuscarTodos(Expression<Func<ProdutosModalidades, bool>> func, int pagina = 0);
        Task<List<ProdutosModalidades>> BuscarTodosComNoLock(Expression<Func<ProdutosModalidades, bool>> func, int pagina = 0);
        Task<ProdutosModalidades> BuscarId(int id);
    }
}
