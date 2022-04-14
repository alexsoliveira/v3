using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TGS.Cartorio.Domain.Interfaces.Services
{
    public interface  ITiposDocumentosPCService
    {
        Task<List<TiposDocumentosPc>> BuscarTodos(int pagina = 0);
        Task<List<TiposDocumentosPc>> BuscarTodosComNoLock(int pagina = 0);
        Task<TiposDocumentosPc> BuscarId(int IdTipoDocumento);
    }
}
