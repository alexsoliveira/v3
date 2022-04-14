using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities.Procuracoes;

namespace TGS.Cartorio.Domain.Interfaces.Services.Strategy
{
    public interface IRegrasOutorgantesStrategy
    {
        Task CriaOutorgante(Outorgante data);
    }
}
