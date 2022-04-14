using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Application.DTO;
using TGS.Cartorio.Application.DTO.Products.Matrimonio;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Application.AppServices.Interfaces
{
    public interface ICarrinhoAppService
    {
        Task<Usuarios> BuscarSolicitante(long id);
        Task<ProdutosDto> ObterProduto(long id);
        Task<List<ComposicaoPrecoDTO>> ObterComposicaoPrecos(long id);
        Task<TermoConcordanciaDTO> ObterTermoConcordancia(string descricao);
        Task AceiteTermoConcordancia(long idSolicitacao, bool isTermoAceito);
        Task<IEnumerable<ParticipantesDto>> ObterParticipantes(long idSolicitacao);
    }
}
