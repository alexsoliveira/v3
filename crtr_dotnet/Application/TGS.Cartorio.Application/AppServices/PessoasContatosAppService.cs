using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Application.AppServices
{
    public class PessoasContatosAppService : IPessoasContatosAppService
    {
        private readonly IPessoasContatosService _pessoaContatoService;
        public PessoasContatosAppService(IPessoasContatosService pessoaContatoService)
        {
            _pessoaContatoService = pessoaContatoService;
        }

        public async Task Incluir(PessoasContatos pessoacontatos)
        {
            await _pessoaContatoService.Incluir(pessoacontatos);
        }
        public async Task Atualizar(PessoasContatos pessoacontatos)
        {
            await _pessoaContatoService.Atualizar(pessoacontatos);
        }

        public async Task<PessoasContatos> BuscarId(long id)
        {
            return await _pessoaContatoService.BuscarId(id);
        }

        public async Task<IEnumerable<PessoasContatos>> BuscarPorPessoa(long idPessoa)
        {
            return await _pessoaContatoService.BuscarPorPessoa(idPessoa);
        }

        public async Task<List<PessoasContatos>> BuscarTodos(int pagina = 0)
        {
            return await _pessoaContatoService.BuscarTodos(pagina);
        }

        public async Task<List<PessoasContatos>> BuscarTodosComNoLock(int pagina = 0)
        {
            return await _pessoaContatoService.BuscarTodosComNoLock(pagina);
        }
    }
}
