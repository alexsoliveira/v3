using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Domain.Interfaces.Services
{
    public interface ILogSistemaService
    {
        Task Add(LogSistema log);
        void AddByJob(LogSistema log);
    }
}
