using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Services;


namespace TGS.Cartorio.Domain.Services
{
	public class ProdutosDocumentosService : IProdutosDocumentosService
    {
        private readonly IProdutosDocumentosSqlRepository _produtosTemplatesDocumentosRepository;

        public ProdutosDocumentosService(IProdutosDocumentosSqlRepository produtosTemplatesDocumentosRepository)
        {
            _produtosTemplatesDocumentosRepository = produtosTemplatesDocumentosRepository;
        }

        public async Task Incluir(ProdutosDocumentos ProdutosAtosCartoriais)
        {
            await _produtosTemplatesDocumentosRepository.Incluir(ProdutosAtosCartoriais);
        }
        public async Task Atualizar(ProdutosDocumentos ProdutosAtosCartoriais)
        {
            ProdutosAtosCartoriais.DataOperacao = DateTime.Now;
            await _produtosTemplatesDocumentosRepository.Atualizar(ProdutosAtosCartoriais);
        }

        public async Task<ProdutosDocumentos> BuscarId(int id)
        {
            return await _produtosTemplatesDocumentosRepository.BuscarId(id);
        }

        public async Task<List<ProdutosDocumentos>> BuscarTodos(int pagina)
        {
            return await _produtosTemplatesDocumentosRepository.BuscarTodos(u => true, pagina);
        }

        public async Task<List<ProdutosDocumentos>> BuscarTodosComNoLock(int pagina)
        {
            return await _produtosTemplatesDocumentosRepository.BuscarTodosComNoLock(u => true, pagina);
        }

    }
}
