using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Application.AppServices.Interfaces
{
    public interface ISolicitacoesDocumentosAppService
    {
        Task AssinarDocumento(SolicitacoesDocumentos solicitacaodocumento);

        Task Incluir(SolicitacoesDocumentos solicitacaodocumento);

        Task Atualizar(SolicitacoesDocumentos solicitacaodocumento);

        Task<List<SolicitacoesDocumentos>> BuscarTodos(int pagina = 0);
        Task<List<SolicitacoesDocumentos>> BuscarTodosComNoLock(int pagina = 0);
        Task<SolicitacoesDocumentos> BuscarId(long id);
        
    }
}