using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Domain.Services
{

    public class TiposPartesPCService : ITiposPartesPCService
    {
        private readonly ITiposPartesPCSqlRepository _tiposPartesPCRepository;

        public TiposPartesPCService(ITiposPartesPCSqlRepository tiposPartesPCRepository)
        {
            _tiposPartesPCRepository = tiposPartesPCRepository;
        }

        public async Task<List<TiposPartesPc>> BuscarTodos(int pagina)
        {
            return await _tiposPartesPCRepository.BuscarTodos(u => true, pagina);
        }

        public async Task<List<TiposPartesPc>> BuscarTodosComNoLock(int pagina)
        {
            return await _tiposPartesPCRepository.BuscarTodosComNoLock(u => true, pagina);
        }

        public async Task<List<TiposPartesPc>> BuscarTodos(Expression<Func<TiposPartesPc, bool>> func, int pagina)
        {
            return await _tiposPartesPCRepository.BuscarTodos(func, pagina);
        }
    }
}
