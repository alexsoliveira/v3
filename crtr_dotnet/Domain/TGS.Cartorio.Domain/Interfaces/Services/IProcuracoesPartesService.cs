using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Entities.Procuracoes;

namespace TGS.Cartorio.Domain.Interfaces.Services
{
    public interface IProcuracoesPartesService
    {
        Task Incluir(ProcuracoesPartes procuracoesParte);
        Task<ProcuracoesPartes> BuscarPorId(long id);
        Task<IEnumerable<ProcuracoesPartes>> BuscarPorIdSolicitacao(long idSolicitacao);
        IEnumerable<ProcuracoesPartes> BuscarPorIdSolicitacaoByJob(long idSolicitacao);
        Task<IEnumerable<Participantes>> ObterParticipantes(long idSolicitacao);
        Task<IEnumerable<Outorgante>> AtualizarOutorgantes(SolicitacoesOutorgantes solicitacaoOutorgantes);
        Task<IEnumerable<Outorgados>> AtualizarOutorgados(SolicitacoesOutorgados solicitacaoOutorgados);
        Task<bool> ValidarSeExisteProcuracaoParteComMatrimonio(long idPessoaSolicitante, long idMatrimonio);
        Task<ProcuracoesPartes> BuscarSolicitantePorMatrimonio(long idMatrimonio, long idPessoaSolicitante);
    }
}

