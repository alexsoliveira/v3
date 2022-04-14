using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TGS.Cartorio.Domain.Interfaces.Services
{
    public interface  IGenerosPCService
    {
        Task<List<GenerosPc>> BuscarTodos(int pagina = 0);
        Task<List<GenerosPc>> BuscarTodosComNoLock(int pagina = 0);
    }
}
