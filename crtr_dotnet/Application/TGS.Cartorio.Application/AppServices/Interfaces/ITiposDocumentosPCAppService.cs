using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Application.AppServices.Interfaces
{
    public interface ITiposDocumentosPcAppService
    {
        Task<List<TiposDocumentosPc>> BuscarTodos(int pagina = 0);
        Task<List<TiposDocumentosPc>> BuscarTodosComNoLock(int pagina = 0);
        Task<TiposDocumentosPc> BuscarId(int IdTipoDocumento);
    }
}