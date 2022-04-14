using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities.Procuracoes;

namespace TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer.Procuracoes
{
    public interface IMatrimoniosSqlRepository
    {
        Task Incluir(Matrimonios matrimonios);
        Task<Matrimonios> BuscarPorSolicitacao(long idSolicitacao);
        Matrimonios BuscarPorSolicitacaoByJob(long idSolicitacao);
        Task Atualizar(Matrimonios matrimonios);
        Task<bool> Existe(long idMatrimonio);
        Task<Matrimonios> BuscarPorId(long idMatrimonio);
    }
}
