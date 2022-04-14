using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer
{
    public interface ILogSistemaSqlRepository : ISqlRepository<LogSistema>
    {
        Task Add(LogSistema log);
        void AddByJob(LogSistema log);
    }
}