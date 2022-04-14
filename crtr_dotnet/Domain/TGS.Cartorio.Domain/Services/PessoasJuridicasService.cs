using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Domain.Services
{

    public class PessoasJuridicasService : IPessoasJuridicasService
    {
        private readonly IPessoasJuridicasSqlRepository _pessoasJuridicasRepositorio;

        public PessoasJuridicasService(IPessoasJuridicasSqlRepository pessoasJuridicasRepositorio)
        {
            _pessoasJuridicasRepositorio = pessoasJuridicasRepositorio;
        }

        public async Task Incluir(PessoasJuridicas pessoajuridica)
        {
            await _pessoasJuridicasRepositorio.Incluir(pessoajuridica);
        }
        public async Task Atualizar(PessoasJuridicas pessoajuridica)
        {
            pessoajuridica.DataOperacao = DateTime.Now;
            await _pessoasJuridicasRepositorio.Atualizar(pessoajuridica);
        }

        public async Task<PessoasJuridicas> BuscarId(long id)
        {
            return await _pessoasJuridicasRepositorio.BuscarId(id);
        }

        public async Task<List<PessoasJuridicas>> BuscarTodos(Expression<Func<PessoasJuridicas, bool>> func, int pagina = 0)
        {
            return await _pessoasJuridicasRepositorio.BuscarTodos(func, pagina);
        }

        public async Task<List<PessoasJuridicas>> BuscarTodosComNoLock(int pagina = 0)
        {
            return await _pessoasJuridicasRepositorio.BuscarTodosComNoLock(u => true, pagina);

        }

        public async  Task<List<PessoasJuridicas>> BuscarTodos(int pagina)
        {
            return await _pessoasJuridicasRepositorio.BuscarTodos(u => true, pagina);
        }
    }
}
