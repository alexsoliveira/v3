using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities.Procuracoes;

namespace TGS.Cartorio.Domain.Interfaces.Services
{
    public interface IProcuracoesPartesEstadosService
    {
        Task Incluir(ProcuracoesPartesEstados procuracoesPartesEstados);
        Task<ProcuracoesPartesEstados> BuscarId(long id);
        Task<IEnumerable<ProcuracoesPartesEstados>> BuscarPorProcuracaoParte(long idProcuracaoParte);
    }
}

