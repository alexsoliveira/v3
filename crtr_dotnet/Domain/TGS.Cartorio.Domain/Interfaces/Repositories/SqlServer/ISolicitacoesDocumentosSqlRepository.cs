using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer
{
    public interface ISolicitacoesDocumentosSqlRepository : ISqlRepository<SolicitacoesDocumentos>
    {
        Task Incluir(SolicitacoesDocumentos solicitacaodocumento);
		Task Atualizar(SolicitacoesDocumentos solicitacaodocumento);
        Task<SolicitacoesDocumentos> BuscarId(long id);
        Task<List<SolicitacoesDocumentos>> BuscarTodos(Expression<Func<SolicitacoesDocumentos, bool>> func, int pagina = 0);
        Task<List<SolicitacoesDocumentos>> BuscarTodosComNoLock(Expression<Func<SolicitacoesDocumentos, bool>> func, int pagina = 0);
        Task DeletarId(long id);
    }
}