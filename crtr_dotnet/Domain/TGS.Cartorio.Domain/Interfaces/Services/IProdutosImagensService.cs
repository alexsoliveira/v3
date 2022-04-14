using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace TGS.Cartorio.Domain.Interfaces.Services
{
    public interface IProdutosImagensService
    {
        Task Incluir(ProdutosImagens produtoimagem);
        Task Atualizar(ProdutosImagens produtoimagem);

        Task<List<ProdutosImagens>> BuscarTodos(int pagina);
        Task<List<ProdutosImagens>> BuscarTodosComNoLock(int pagina);
        Task<ProdutosImagens> BuscarId(int id);
        
    }
}
