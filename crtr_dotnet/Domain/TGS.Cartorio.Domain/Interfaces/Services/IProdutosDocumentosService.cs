using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TGS.Cartorio.Domain.Interfaces.Services
{
    public interface  IProdutosDocumentosService
    {
        Task Incluir(ProdutosDocumentos produtosdocumentos);
        Task Atualizar(ProdutosDocumentos produtosdocumentos);

        Task<List<ProdutosDocumentos>> BuscarTodos(int pagina);
        Task<List<ProdutosDocumentos>> BuscarTodosComNoLock(int pagina);
        Task<ProdutosDocumentos> BuscarId(int id);
    }
}
