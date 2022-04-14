using System;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer
{
    public interface ISqlRepository<T> : IDisposable where T : class
    {        
        IUnitOfWork UnitOfWork { get; }
    }
}
