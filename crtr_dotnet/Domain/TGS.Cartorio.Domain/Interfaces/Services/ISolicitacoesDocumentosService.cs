using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TGS.Cartorio.Domain.Interfaces.Services
{
    public interface  ISolicitacoesDocumentosService
    {
        Task Incluir(SolicitacoesDocumentos solicitacaodocumento);
        Task Atualizar(SolicitacoesDocumentos solicitacaodocumento);

        Task<List<SolicitacoesDocumentos>> BuscarTodos(int pagina = 0);
        Task<List<SolicitacoesDocumentos>> BuscarTodosComNoLock(int pagina = 0);
        Task<SolicitacoesDocumentos> BuscarId(long id);
        Task AssinarDocumento(SolicitacoesDocumentos solicitacaodocumento);
        Task ValidarSolicitacao(long idsolicitacao);

        Task DeletarId(long id);
    }
}
