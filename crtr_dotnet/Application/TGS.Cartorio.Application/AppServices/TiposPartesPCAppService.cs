using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Services;


namespace TGS.Cartorio.Application.AppServices
{
    public class TiposPartesPCAppService : ITiposPartesPCAppService
    {
        private readonly ITiposPartesPCService _tiposPartesPCService;

        public TiposPartesPCAppService(ITiposPartesPCService tiposPartesPCService)
        {
            _tiposPartesPCService = tiposPartesPCService;
        }

        public async Task<List<TiposPartesPc>> BuscarTodos(int pagina)
        {
            return await _tiposPartesPCService.BuscarTodos(p => true, pagina);
        }

        public async Task<List<TiposPartesPc>> BuscarTodosComNoLock(int pagina)
        {
            return await _tiposPartesPCService.BuscarTodosComNoLock(pagina);
        }

        public async Task<List<TiposPartesPc>> BuscarTodos(Expression<Func<TiposPartesPc, bool>> func, int pagina)
        {
            return await _tiposPartesPCService.BuscarTodos(func, pagina);
        }
    }
}