using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities.Procuracoes;
using TGS.Cartorio.Domain.Entities.Procuracoes.Produtos.Matrimonio;

namespace TGS.Cartorio.Domain.Interfaces.Services
{
    public interface IMatrimoniosService
    {
        Task<long> Incluir(DadosMatrimonio matrimonios);
        Task<Matrimonios> BuscarPorSolicitacao(long idSolicitacao);
        Matrimonios BuscarPorSolicitacaoByJob(long idSolicitacao);
        Task<long> Atualizar(DadosMatrimonio dadosMatrimonio);
        Task<bool> Existe(long idMatrimonio);
        Task<Matrimonios> BuscarPorId(long idMatrimonio);
    }
}
