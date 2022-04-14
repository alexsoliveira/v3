using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Entities.Procuracoes;

namespace TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer.Procuracoes
{
    public interface IProcuracoesPartesSqlRepository
    {
        Task<List<ProcuracoesPartes>> MinhasParticipacoes(long id);
        Task<List<StatusSolicitacao>> BuscarEstadoDoPedidoPorParticipante(long idSolicitacao);
        Task<List<ProcuracoesPartesEstadosPc>> BuscarTodasProcuracoesPartesEstados(Expression<Func<ProcuracoesPartesEstadosPc, bool>> func, int pagina = 0);
        Task Incluir(ProcuracoesPartes procuracoesParte);
        Task<ProcuracoesPartes> BuscarPorId(long id);
        Task<IEnumerable<ProcuracoesPartes>> BuscarPorIdSolicitacao(long idSolicitacao);
        IEnumerable<ProcuracoesPartes> BuscarPorIdSolicitacaoByJob(long idSolicitacao);
        Task<IEnumerable<Participantes>> ObterParticipantes(long idSolicitacao);
        Task Atualizar(ProcuracoesPartes parte);
        Task Remover(ProcuracoesPartes parte);
        Task<bool> ValidarSeExisteProcuracaoParteComMatrimonio(long idPessoaSolicitante, long idMatrimonio);
        Task<ProcuracoesPartes> BuscarPorSolicitante(long idSolicitacao, long idPessoaSolicitante);
    }
}
