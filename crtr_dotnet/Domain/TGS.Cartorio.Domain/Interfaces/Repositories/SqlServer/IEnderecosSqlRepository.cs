using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;
namespace TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer
{
    public interface  IEnderecosSqlRepository : ISqlRepository<Enderecos>
    {
        Task Incluir(Enderecos endereco);
		Task Atualizar(Enderecos endereco);
        Task Apagar(int Idendereco);
        Task<Enderecos> BuscarId(int id);
        Task<List<Enderecos>> BuscarTodos(Expression<Func<Enderecos, bool>> func, int pagina = 0);
        Task<List<Enderecos>> BuscarTodosComNoLock(Expression<Func<Enderecos, bool>> func, int pagina = 0);
    }
}
