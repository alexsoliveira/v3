using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Services;
namespace TGS.Cartorio.Domain.Services
{
    
	  public class PessoasService : IPessoasService
    {
		    private readonly IPessoasSqlRepository _pessoasRepository;

        public PessoasService(IPessoasSqlRepository pessoasRepository)
        {
            _pessoasRepository = pessoasRepository;
        }

        public async Task Incluir(Pessoas pessoa)
        {            
            await _pessoasRepository.Incluir(pessoa);
        }

        public async Task<Pessoas> BuscarId(long id)
        {
            return await _pessoasRepository.BuscarId(id);
        }

        public async Task<Pessoas> BuscarPorIdCompleto(long id)
        {
            return await _pessoasRepository.BuscarPorIdCompleto(id);
        }

        public async Task<List<Pessoas>> BuscarTodos(int pagina = 0)
        {
            return await _pessoasRepository.BuscarTodos(u => true,pagina);
        }

       

        public async Task<List<Pessoas>> BuscarTodosComNoLock(int pagina = 0)
        {
            return await _pessoasRepository.BuscarTodosComNoLock(u => true,pagina);            
        }
        public async Task Atualizar(Pessoas pessoa)
        {
            pessoa.DataOperacao = DateTime.Now;
            await _pessoasRepository.Atualizar(pessoa);
        }

        public async Task<List<Pessoas>> BuscarTodosComNoLock(Expression<Func<Pessoas, bool>> func, int pagina)
        {
            return await _pessoasRepository.BuscarTodosComNoLock(func, pagina);
        }

        public async Task<long?> PessoaExiste(int idTipoDocumento, long documento)
        {
            return await _pessoasRepository.ExisteAsync(idTipoDocumento, documento);
        }
    }
}
