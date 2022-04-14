using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities.Procuracoes;

namespace TGS.Cartorio.Domain.Interfaces.Services
{
    public interface  ISolicitacoesService
    {
        Task Incluir(Solicitacoes solicitacao);
        Task Atualizar(Solicitacoes solicitacao);
        Task<List<Solicitacoes>> BuscarTodos(int pagina = 0);
        Task<List<Solicitacoes>> BuscarTodosComNoLock(Expression<Func<Solicitacoes, bool>> func, int pagina = 0);
        Task<Solicitacoes> BuscarId(long id);
        //Task LiberarAtendimento(long IdSolicitacao);
        Task EmAtendimento(long idsolicitacao);
        Task Reprovar(long idsolicitacao);
        Task Aprovar(long idsolicitacao);
        Task AguardandoAprovacaoMinuta(long idsolicitacao);
        Task<StatusSolicitacaoHeader> BuscarDadosStatusSolicitacao(long idSolicitacao);
        Task<List<MinhasSolicitacoes>> MinhasSolicitacoes(long id);
        Task<MinhaSolicitacao> ConsultarBoleto(long id);
        Task<List<StatusSolicitacao>> BuscarEstadoDoPedidoPorParticipante(long idSolicitacao);
        Task<List<ProcuracoesPartesEstadosPc>> BuscarTodasProcuracoesPartesEstados(Expression<Func<ProcuracoesPartesEstadosPc, bool>> func, int pagina = 0);
        Task<List<Solicitacoes>> Pesquisar(Expression<Func<Solicitacoes, bool>> func, int pagina = 0);
        List<Solicitacoes> BuscarPorSolicitacoesProntasParaEnvioCartorio();
        void SolicitacaoEnviadaAoCartorio(Solicitacoes solicitacao);
        Task<IEnumerable<Solicitacoes>> TodasSolicitacoesAguardandoPagamentoBoleto();
        void AtualizarSolicitacaoPorJob(Solicitacoes solicitacao);
        void FinalizarJob();
    }
}
