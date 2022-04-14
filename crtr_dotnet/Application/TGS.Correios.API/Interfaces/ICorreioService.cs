using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Correios.API.DTO;

namespace TGS.Correios.API.Interfaces
{
    public interface ICorreioService
    {
        [Get("/ws/{cep}/json/")]
        Task<Endereco> ConsultarEnderecoAsync(string cep);

        [Get("/ws/{uf}/{cidade}/{logradouro}/json/")]
        Task<IList<Endereco>> ConsultarCEPAsync(string uf, string cidade, string logradouro);
    }
}
