using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Application.AppServices
{
    public class PessoasJuridicasAppService : IPessoasJuridicasAppService
    {
        private readonly IPessoasService _pessoaService;
        private readonly IPessoasJuridicasService _pessoaJuridicaService;
        public PessoasJuridicasAppService(IPessoasJuridicasService pessoaJuridicaService,
            IPessoasService pessoaService
            )
        {
            _pessoaJuridicaService = pessoaJuridicaService;
            _pessoaService = pessoaService;
        }

        public async Task Incluir(PessoasJuridicas pessoajuridica)
        {
            await _pessoaJuridicaService.Incluir(pessoajuridica);
        }
        public async Task Atualizar(PessoasJuridicas pessoajuridica)
        {
            await _pessoaJuridicaService.Atualizar(pessoajuridica);
        }

        public async Task<List<PessoasJuridicas>> BuscarTodos(Expression<Func<PessoasJuridicas, bool>> func, int pagina = 0)
        {
            return await _pessoaJuridicaService.BuscarTodos(func, pagina);
        }

        public async Task<PessoasJuridicas> BuscarId(long id)
        {
            return await _pessoaJuridicaService.BuscarId(id);
        }


        public async Task<List<PessoasJuridicas>> BuscarTodosComNoLock(int pagina = 0)
        {
            return await _pessoaJuridicaService.BuscarTodosComNoLock(pagina);

        }

        public async Task<List<PessoasJuridicas>> BuscarTodos(int pagina = 0)
        {
            return await _pessoaJuridicaService.BuscarTodos(pagina);
        }
    }
}
