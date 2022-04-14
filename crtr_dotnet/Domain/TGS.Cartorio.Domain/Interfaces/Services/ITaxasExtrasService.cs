using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Domain.Interfaces.Services
{
    public interface ITaxasExtrasService
    {
        Task<TaxasExtras> BuscarTaxaEmolumentoPorEstado(string uf);
    }
}
