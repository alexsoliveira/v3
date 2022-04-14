using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Domain.Services
{
	
    public class PessoasContatosService : IPessoasContatosService 
    {
        private readonly IPessoasContatosSqlRepository _pessoasContatosRepository;

        public PessoasContatosService(IPessoasContatosSqlRepository pessoasContatosRepository)
        {
            _pessoasContatosRepository = pessoasContatosRepository;
        }

        public async Task Incluir(PessoasContatos pessoacontato)
        {
            await _pessoasContatosRepository.Incluir(pessoacontato);
        }

        public async Task Atualizar(PessoasContatos pessoacontato)
        {
            pessoacontato.DataOperacao = DateTime.Now;
            await _pessoasContatosRepository.Atualizar(pessoacontato);
        }

        public async Task<PessoasContatos> BuscarId(long id)
        {
            return await _pessoasContatosRepository.BuscarId(id);
        }

        public async Task<IEnumerable<PessoasContatos>> BuscarPorPessoa(long idPessoa)
        {
            return await _pessoasContatosRepository.BuscarPorPessoa(idPessoa);
        }

        public async Task<List<PessoasContatos>> BuscarTodos(int pagina = 0)
        {
            return await _pessoasContatosRepository.BuscarTodos(u => true, pagina);
        }

        public async Task<List<PessoasContatos>> BuscarTodosComNoLock(int pagina = 0)
        {
            return await _pessoasContatosRepository.BuscarTodosComNoLock(u => true, pagina);

        }
    }
}
