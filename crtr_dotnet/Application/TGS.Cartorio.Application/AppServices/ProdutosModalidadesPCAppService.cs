using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Services;


namespace TGS.Cartorio.Application.AppServices
{
    public class ProdutosModalidadesPcAppService : IProdutosModalidadesPcAppService
    {
        private readonly IProdutosModalidadesPCService _produtosModalidadesPCService;
		
        public ProdutosModalidadesPcAppService(IProdutosModalidadesPCService produtosModalidadesPCService)
        {
            _produtosModalidadesPCService = produtosModalidadesPCService;
        }
        
        public async Task<List<ProdutosModalidadesPc>> BuscarTodos(int pagina)
        {
            return await _produtosModalidadesPCService.BuscarTodos(pagina);
        }

        public async Task<List<ProdutosModalidadesPc>> BuscarTodos()
        {
            return await _produtosModalidadesPCService.BuscarTodos();
        }

        public async Task<List<ProdutosModalidadesPc>> BuscarTodosComNoLock(int pagina)
        {
            return await _produtosModalidadesPCService.BuscarTodosComNoLock(pagina);
        }
        public async Task<ProdutosModalidadesPc> BuscarId(int id)
        {
            return await _produtosModalidadesPCService.BuscarId(id);
        }

    }
}