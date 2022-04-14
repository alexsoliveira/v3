using System.ServiceModel;
using System.Threading.Tasks;

namespace TGS.Cartorio.Infrastructure.Utility.WebServerContracts.Contracts
{
    [ServiceContract]
    public interface IEmailContract
    {
        [OperationContract(Action = "http://tempuri.org/IServicoEmail/ProcessarPedido")]
        Task<string> ProcessarPedidoAsync(byte[] arquivo);
    }
}
