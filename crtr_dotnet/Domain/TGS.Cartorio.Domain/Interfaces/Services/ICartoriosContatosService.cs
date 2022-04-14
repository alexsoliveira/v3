using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TGS.Cartorio.Domain.Interfaces.Services
{
    public interface  ICartoriosContatosService
    {
        Task Atualizar(CartoriosContatos cartorio);
        Task Incluir(CartoriosContatos cartorio);
        Task<List<CartoriosContatos>> BuscarTodos(int pagina = 0);
        Task<List<CartoriosContatos>> BuscarTodosComNoLock(int pagina = 0);
        Task<CartoriosContatos> BuscarId(int id);
    }
}
