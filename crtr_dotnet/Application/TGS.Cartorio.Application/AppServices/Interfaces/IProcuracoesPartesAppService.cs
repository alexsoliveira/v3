using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Application.DTO;

namespace TGS.Cartorio.Application.AppServices.Interfaces
{
    public interface IProcuracoesPartesAppService
    {
        Task Incluir(ProcuracoesPartesDto procuracoesPartesDto);
        Task<ProcuracoesPartesDto> BuscarPorId(long id);
        Task<IEnumerable<ProcuracoesPartesDto>> BuscarPorIdSolicitacao(long idSolicitacao);
        Task AtualizarOutorgantes(SolicitacoesOutorgantesDto solicitacaoOutorgantes);
        Task AtualizarOutorgados(SolicitacoesOutorgadosDto solicitacaoOutorgados);
    }
}