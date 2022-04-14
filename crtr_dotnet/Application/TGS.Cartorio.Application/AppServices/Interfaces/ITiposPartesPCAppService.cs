using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;
using System.Linq.Expressions;
using System;

namespace TGS.Cartorio.Application.AppServices.Interfaces
{
    public interface ITiposPartesPCAppService
    {
        Task<List<TiposPartesPc>> BuscarTodos(Expression<Func<TiposPartesPc, bool>> func, int pagina = 0);
        Task<List<TiposPartesPc>> BuscarTodos(int pagina = 0);
        Task<List<TiposPartesPc>> BuscarTodosComNoLock(int pagina = 0);
    }
}