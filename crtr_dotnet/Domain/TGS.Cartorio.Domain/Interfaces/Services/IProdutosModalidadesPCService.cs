using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TGS.Cartorio.Domain.Interfaces.Services
{
    public interface  IProdutosModalidadesPCService
    {
        Task<ProdutosModalidadesPc> BuscarId(int id);
        Task<List<ProdutosModalidadesPc>> BuscarTodos(int pagina = 0);
        Task<List<ProdutosModalidadesPc>> BuscarTodos();
        Task<List<ProdutosModalidadesPc>> BuscarTodosComNoLock(int pagina = 0);
    }
}
