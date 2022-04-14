using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TGS.Cartorio.Domain.Interfaces.Services
{
    public interface  ITiposPartesPCService
    {
        
        Task<List<TiposPartesPc>> BuscarTodos(Expression<Func<TiposPartesPc, bool>> func, int pagina = 0);
        Task<List<TiposPartesPc>> BuscarTodosComNoLock(int pagina = 0);
    }
}
