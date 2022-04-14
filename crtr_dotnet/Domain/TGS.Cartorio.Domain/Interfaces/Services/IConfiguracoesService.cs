using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Domain.Interfaces.Services
{
    public interface IConfiguracoesService
    {
        Task<List<Configuracoes>> BuscarTodos(int pagina = 0);
        Task<List<Configuracoes>> BuscarTodos(Expression<Func<Configuracoes, bool>> func, int pagina = 0);
        Task<List<Configuracoes>> BuscarTodosComNoLock(int pagina = 0);
        Task<Configuracoes> BuscarPorDescricao(Expression<Func<Configuracoes, bool>> func);
    }
}
