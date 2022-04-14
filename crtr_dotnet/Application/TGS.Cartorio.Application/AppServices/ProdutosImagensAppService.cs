using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Application.AppServices
{
    public class ProdutosImagensAppService : IProdutosImagensAppService
    {
        private readonly IProdutosImagensService _produtoImagemService;

        public ProdutosImagensAppService(IProdutosImagensService produtoImagemService)
        {
            _produtoImagemService = produtoImagemService;
        }

        public async Task Incluir(ProdutosImagens produtoimagem)
        {
            await _produtoImagemService.Incluir(produtoimagem);
        }
        public async Task Atualizar(ProdutosImagens produtoimagem)
        {
            await _produtoImagemService.Atualizar(produtoimagem);
        }
        public async Task<ProdutosImagens> BuscarId(int id)
        {
            return await _produtoImagemService.BuscarId(id);
        }

        public async Task<List<ProdutosImagens>> BuscarTodos(int pagina)
        {
            return await _produtoImagemService.BuscarTodos(pagina);
        }

        public async Task<List<ProdutosImagens>> BuscarTodosComNoLock(int pagina)
        {
            return await _produtoImagemService.BuscarTodosComNoLock(pagina);
        }
    }
}
