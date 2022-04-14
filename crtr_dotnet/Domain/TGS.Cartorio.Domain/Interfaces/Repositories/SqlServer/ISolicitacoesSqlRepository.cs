using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;


namespace TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer
{
    public interface ISolicitacoesSqlRepository : ISqlRepository<Solicitacoes>
    {
        Task Incluir(Solicitacoes solicitacao);
		Task Atualizar(Solicitacoes solicitacao);
        Task<Solicitacoes> BuscarId(long id);
        Task<StatusSolicitacaoHeader> BuscarDadosStatusSolicitacao(long idSolicitacao);
        Task<List<Solicitacoes>> BuscarTodos(Expression<Func<Solicitacoes, bool>> func, int pagina = 0);
        Task<List<Solicitacoes>> BuscarTodosComNoLock(Expression<Func<Solicitacoes, bool>> func, int pagina = 0);
        List<Solicitacoes> BuscarPorSolicitacoesProntasParaEnvioCartorio();
        Task<List<MinhasSolicitacoes>> MinhasSolicitacoes(long idPessoa);
        Task<MinhaSolicitacao> ConsultarBoleto(long idSolicitacao);
        Task<IEnumerable<Solicitacoes>> TodasSolicitacoesAguardandoPagamentoBoleto();
        void AtualizarViaJob(Solicitacoes solicitacao);
        void FinalizarJob();
    }
}