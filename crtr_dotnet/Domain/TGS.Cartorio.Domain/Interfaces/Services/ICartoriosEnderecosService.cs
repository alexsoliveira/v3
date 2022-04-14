using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TGS.Cartorio.Domain.Interfaces.Services
{
    public interface  ICartoriosEnderecosService
    {
        Task Atualizar(CartoriosEnderecos cartorioendereco);

        Task Incluir(CartoriosEnderecos cartorioendereco);
        Task<List<CartoriosEnderecos>> BuscarTodos(int pagina = 0);
        Task<List<CartoriosEnderecos>> BuscarTodosComNoLock(int pagina = 0);
        Task<CartoriosEnderecos> BuscarId(int id);
    }
}
