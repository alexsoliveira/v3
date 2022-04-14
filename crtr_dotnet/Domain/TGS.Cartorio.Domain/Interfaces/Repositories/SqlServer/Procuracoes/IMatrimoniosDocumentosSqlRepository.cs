using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities.Procuracoes;

namespace TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer.Procuracoes
{
    public interface IMatrimoniosDocumentosSqlRepository
    {
        Task Incluir(MatrimoniosDocumentos matrimoniosDocumentos);
        Task<IEnumerable<MatrimoniosDocumentos>> BuscarPorMatrimonio(long idMatrimonio);
        IEnumerable<MatrimoniosDocumentos> BuscarPorMatrimonioByJob(long idMatrimonio);
        Task<ICollection<MatrimoniosDocumentos>> BuscarPorSolicitacao(long idSolicitacao);
        Task Remover(MatrimoniosDocumentos matrimoniosDocumentos);
        Task<MatrimoniosDocumentos> BuscarPorId(long idMatrimonioDocumento);
        Task Atualizar(MatrimoniosDocumentos matrimoniosDocumentos);
        Task<MatrimoniosDocumentos> BuscarPorMatrimonioComProcuracaoParte(long idMatrimonio, long idPessoaSolicitante);
    }
}
