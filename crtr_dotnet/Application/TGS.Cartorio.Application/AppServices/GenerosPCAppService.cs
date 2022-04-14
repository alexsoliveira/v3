using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Services;


namespace TGS.Cartorio.Application.AppServices
{
    public class GenerosPcAppService : IGenerosPcAppService
    {
        private readonly IGenerosPCService _generosPCService;
		
        public GenerosPcAppService(IGenerosPCService generosPCService)
        {
            _generosPCService = generosPCService;
        }
        
        public async Task<List<GenerosPc>> BuscarTodos(int pagina)
        {
            return await _generosPCService.BuscarTodos(pagina);
        }

        public async Task<List<GenerosPc>> BuscarTodosComNoLock(int pagina)
        {
            return await _generosPCService.BuscarTodosComNoLock(pagina);
        }

    }
}