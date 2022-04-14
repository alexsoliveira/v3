using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Domain.Services
{
    
	public class ProdutosImagensService : IProdutosImagensService
    {
        private readonly IProdutosImagensSqlRepository _produtosImagensRepository;

        public ProdutosImagensService(IProdutosImagensSqlRepository produtosImagensRepository)
        {
            _produtosImagensRepository = produtosImagensRepository;
        }

        public async Task Incluir(ProdutosImagens produtoimagem)
        {
            await _produtosImagensRepository.Incluir(produtoimagem);
        }
        public async Task Atualizar(ProdutosImagens produtoimagem)
        {
            produtoimagem.DataOperacao = DateTime.Now;
            await _produtosImagensRepository.Atualizar(produtoimagem);
        }

        

        public async Task<ProdutosImagens> BuscarId(int id)
        {
            return await _produtosImagensRepository.BuscarId(id);
        }

        public async Task<List<ProdutosImagens>> BuscarTodos(int pagina)
        {
            return await _produtosImagensRepository.BuscarTodos(u=> true, pagina);
        }

        public async Task<List<ProdutosImagens>> BuscarTodosComNoLock(int pagina)
        {
            return await _produtosImagensRepository.BuscarTodosComNoLock(u => true, pagina);
        }
    }
}
