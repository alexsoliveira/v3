using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Domain.Services
{
    
	  public class ProdutosModalidadesPCService : IProdutosModalidadesPCService
    {		    
        private readonly IProdutosModalidadesPCSqlRepository _produtosModalidadesPcRepository;

        public ProdutosModalidadesPCService(IProdutosModalidadesPCSqlRepository produtosModalidadesPcRepository)
        {
            _produtosModalidadesPcRepository = produtosModalidadesPcRepository;
        }

        public async Task<List<ProdutosModalidadesPc>> BuscarTodos(int pagina)
        {
            return await _produtosModalidadesPcRepository.BuscarTodos(u => true, pagina);
        }

        public async Task<List<ProdutosModalidadesPc>> BuscarTodos()
        {
            return await _produtosModalidadesPcRepository.BuscarTodos();
        }

        public async Task<List<ProdutosModalidadesPc>> BuscarTodosComNoLock(int pagina)
        {
            return await _produtosModalidadesPcRepository.BuscarTodosComNoLock(u => true, pagina);
        }

        public async Task<ProdutosModalidadesPc> BuscarId(int id)
        {
            return await _produtosModalidadesPcRepository.BuscarId(id);
        }
    }
}
