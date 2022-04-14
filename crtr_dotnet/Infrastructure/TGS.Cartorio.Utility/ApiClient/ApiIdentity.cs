using System.Net.Http;
using System.Threading.Tasks;

namespace TGS.Cartorio.Infrastructure.Utility.ApiClient
{
    public class ApiIdentity : ApiClientBase
    {
        public ApiIdentity(HttpClient client):base(client)
        {

        }
       
    }
}
