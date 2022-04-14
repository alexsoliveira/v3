using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Application.AppServices.Interfaces
{
    public interface ICartoriosAppService
    {
        Task Atualizar(Cartorios cartorio);
        Task Incluir(Cartorios cartorio);
        Task<List<Cartorios>> BuscarTodos(int pagina = 0);
        Task<List<Cartorios>> BuscarTodosComNoLock(int pagina = 0);
        Task<Cartorios> BuscarId(int id);
    }
}