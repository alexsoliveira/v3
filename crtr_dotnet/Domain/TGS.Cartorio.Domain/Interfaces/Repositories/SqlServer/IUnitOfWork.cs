using System.Threading.Tasks;

namespace TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}