using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Application.DTO;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Infrastructure.Utility.ApiClient;

namespace TGS.Cartorio.Application.AppServices.Interfaces
{
    public interface IEnderecosAppService
    {
        Task Atualizar(Enderecos endereco);
        Task Incluir(Enderecos endereco);
        Task Apagar(int IdEndereco);
        Task<List<Enderecos>> BuscarTodos(int pagina = 0);
        Task<List<Enderecos>> BuscarTodosPorUsuario(int _IdUsuario);
        Task<List<Enderecos>> BuscarTodosComNoLock(int pagina = 0);
        Task<Enderecos> Buscar(Enderecos usuario);
        Task<Retorno<EnderecoDto>> Buscar(string cep);
        Task<Enderecos> BuscarId(int id);
    }
}
