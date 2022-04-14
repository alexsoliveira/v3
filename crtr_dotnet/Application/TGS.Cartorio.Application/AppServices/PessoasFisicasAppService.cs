using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Services;
namespace TGS.Cartorio.Application.AppServices
{
    public class PessoasFisicasAppService : IPessoasFisicasAppService
    {
        private readonly IPessoasService _pessoaService;
        private readonly IPessoasFisicasService _pessoaFisicaService;
        public PessoasFisicasAppService
            (
            IPessoasFisicasService pessoaFisicaService,
            IPessoasService pessoaService
            )
        {
            _pessoaFisicaService = pessoaFisicaService;
            _pessoaService = pessoaService;
        }

        public async Task Incluir(PessoasFisicas pessoafisica)
        {
            await _pessoaFisicaService.Incluir(pessoafisica);
        }
        public async Task Atualizar(PessoasFisicas pessoafisica)
        {
            await _pessoaFisicaService.Atualizar(pessoafisica);
        }

        public async Task<PessoasFisicas> BuscarId(long id)
        {
            return await _pessoaFisicaService.BuscarId(id);
        }

        public async Task<PessoasFisicas> BuscarPorIdPessoa(long idPessoa)
        {
            return await _pessoaFisicaService.BuscarPorIdPessoa(idPessoa);
        }

        public async Task<List<PessoasFisicas>> BuscarTodos(int pagina = 0)
        {
            return await _pessoaFisicaService.BuscarTodos(pagina);
        }

        public async Task<List<PessoasFisicas>> BuscarTodos(Expression<Func<PessoasFisicas, bool>> func, int pagina = 0)
        {
            return await _pessoaFisicaService.BuscarTodos(func, pagina);
        }

        public async Task<List<PessoasFisicas>> BuscarTodosComNoLock(int pagina = 0)
        {
            return await _pessoaFisicaService.BuscarTodosComNoLock(pagina);
        }
    }
}
