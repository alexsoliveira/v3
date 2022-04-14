using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities.Procuracoes;

namespace TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer.Procuracoes
{
    public interface IProcuracoesPartesEstadosSqlRepository : ISqlRepository<ProcuracoesPartesEstados>
    {
        Task Incluir(ProcuracoesPartesEstados procuracoesPartesEstados);
        Task<ProcuracoesPartesEstados> BuscarId(long id);
        Task<IEnumerable<ProcuracoesPartesEstados>> BuscarPorProcuracaoParte(long idProcuracaoParte);
    }
}
