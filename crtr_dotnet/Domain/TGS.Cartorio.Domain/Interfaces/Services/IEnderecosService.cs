using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace TGS.Cartorio.Domain.Interfaces.Services
{
    public interface IEnderecosService
    {
        Task Incluir(Enderecos endereco);
        Task Atualizar(Enderecos endereco);
        Task Apagar(int IdEndereco);
        Task<List<Enderecos>> BuscarTodosComNoLock(int pagina);
        Task<List<Enderecos>> BuscarTodos(int pagina);
        Task<List<Enderecos>> BuscarTodosPorUsuario(int _IdUsuario);        
        Task<Enderecos> Buscar(Enderecos endereco);
        Task<Enderecos> BuscarId(int id);
    }
}
