using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TGS.Cartorio.Domain.Interfaces.Services
{
    public interface  ICartoriosEstadosPCService
    {
        Task<List<CartoriosEstadosPc>> BuscarTodos(int pagina = 0);
        Task<List<CartoriosEstadosPc>> BuscarTodosComNoLock(int pagina = 0);
    }
}
