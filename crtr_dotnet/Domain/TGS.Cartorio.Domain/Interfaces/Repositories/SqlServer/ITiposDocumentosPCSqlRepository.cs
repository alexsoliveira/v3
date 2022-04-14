
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer
{
    public interface ITiposDocumentosPCSqlRepository : ISqlRepository<TiposDocumentosPc>
    {
        Task<List<TiposDocumentosPc>> BuscarTodos(Expression<Func<TiposDocumentosPc, bool>> func, int pagina = 0);
        Task<List<TiposDocumentosPc>> BuscarTodosComNoLock(Expression<Func<TiposDocumentosPc, bool>> func, int pagina = 0);
        
        Task<TiposDocumentosPc> BuscarId(int IdTipoDocumento);
    }
}