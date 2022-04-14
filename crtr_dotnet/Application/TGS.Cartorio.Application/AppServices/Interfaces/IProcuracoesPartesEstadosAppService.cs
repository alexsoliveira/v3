using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Application.DTO;
using TGS.Cartorio.Domain.Entities.Procuracoes;

namespace TGS.Cartorio.Application.AppServices.Interfaces
{
    public interface IProcuracoesPartesEstadosAppService
    {
        Task Incluir(ProcuracoesPartesEstadosDto procuracoesPartesEstadosDto);
        Task<ProcuracoesPartesEstadosDto> BuscarPorId(long id);
        Task<IEnumerable<ProcuracoesPartesEstadosDto>> BuscarPorProcuracaoParte(long idProcuracaoParte);

    }
}