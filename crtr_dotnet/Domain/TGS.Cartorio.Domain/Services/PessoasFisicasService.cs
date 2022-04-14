using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Services;
namespace TGS.Cartorio.Domain.Services
{
    
	public class PessoasFisicasService :IPessoasFisicasService
    {
		private readonly IPessoasFisicasSqlRepository _pessoasFisicasRepositorio;

        public PessoasFisicasService(IPessoasFisicasSqlRepository pessoasFisicasRepositorio)
        {
            _pessoasFisicasRepositorio = pessoasFisicasRepositorio;
        }

        public async Task Incluir(PessoasFisicas pessoafisica)
        {
            await _pessoasFisicasRepositorio.Incluir(pessoafisica);
        }

        public async Task Atualizar(PessoasFisicas pessoafisica)
        {
            pessoafisica.DataOperacao = DateTime.Now;
            await _pessoasFisicasRepositorio.Atualizar(pessoafisica);
        }

        public async Task<PessoasFisicas> BuscarId(long id)
        {
            return await _pessoasFisicasRepositorio.BuscarId(id);
        }

        public async Task<PessoasFisicas> BuscarPorIdPessoa(long idPessoa)
        {
            return await _pessoasFisicasRepositorio.BuscarPorIdPessoa(idPessoa);
        }

        public async Task<List<PessoasFisicas>> BuscarTodos(int pagina = 0)
        {
            return await _pessoasFisicasRepositorio.BuscarTodos(u => true, pagina);
        }

        public async Task<List<PessoasFisicas>> BuscarTodosComNoLock(int pagina = 0)
        {
            return await _pessoasFisicasRepositorio.BuscarTodosComNoLock(u => true, pagina);

        }

        public async  Task<List<PessoasFisicas>> BuscarTodos(Expression<Func<PessoasFisicas, bool>> func, int pagina)
        {
            return await _pessoasFisicasRepositorio.BuscarTodos(func, pagina);
        }
    }
}
