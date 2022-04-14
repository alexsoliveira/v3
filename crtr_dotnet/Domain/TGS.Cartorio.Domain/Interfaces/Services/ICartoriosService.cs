using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TGS.Cartorio.Domain.Interfaces.Services
{
    public interface  ICartoriosService
    {
        Task Atualizar(Cartorios cartorio);
        Task Incluir(Cartorios cartorio);
        Task<List<Cartorios>> BuscarTodos(int pagina = 0);
        Task<List<Cartorios>> BuscarTodosComNoLock(int pagina = 0);
        Task<Cartorios> BuscarId(int id);
        Cartorios BuscarUltimoCartorioValido();
    }
}
