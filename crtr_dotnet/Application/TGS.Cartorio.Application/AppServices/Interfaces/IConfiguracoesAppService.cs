using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Application.AppServices.Interfaces
{
    public interface IConfiguracoesAppService
    {
        Task<List<Configuracoes>> BuscarTodos(int pagina);
        Task<List<Configuracoes>> BuscarTodos(Expression<Func<Configuracoes, bool>> func, int pagina);
        Task<List<Configuracoes>> BuscarTodosComNoLock(int pagina);
        Task<Configuracoes> BuscarPorDescricao(Expression<Func<Configuracoes, bool>> func);
    }
}
