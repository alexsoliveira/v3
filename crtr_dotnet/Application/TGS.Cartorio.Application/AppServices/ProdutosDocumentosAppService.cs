using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Services;
namespace TGS.Cartorio.Application.AppServices
{
    public class ProdutosDocumentosAppService : IProdutosDocumentosAppService
    {
        private readonly IProdutosDocumentosService _produtotemplatedocumentoservice;

        public ProdutosDocumentosAppService(IProdutosDocumentosService produtotemplatedocumentoservice)
        {
            _produtotemplatedocumentoservice = produtotemplatedocumentoservice;
        }

        public async Task Incluir(ProdutosDocumentos produtosdocumentos)
        {
            await _produtotemplatedocumentoservice.Incluir(produtosdocumentos);
        }

        public async Task Atualizar(ProdutosDocumentos produtosdocumentos)
        {
            await _produtotemplatedocumentoservice.Atualizar(produtosdocumentos);
        
        }
        public async Task<List<ProdutosDocumentos>> BuscarTodos(int pagina = 0)
        {
            return await _produtotemplatedocumentoservice.BuscarTodos(pagina);
        }
        public async Task<ProdutosDocumentos> BuscarId(int pagina = 0)
        {
            return await _produtotemplatedocumentoservice.BuscarId(pagina);
        }

        public async Task<List<ProdutosDocumentos>> BuscarTodosComNoLock(int pagina = 0)
        {
            return await _produtotemplatedocumentoservice.BuscarTodosComNoLock(pagina);
        }

    }
}
