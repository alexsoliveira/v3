using System;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Domain.Services
{

    public class LogSistemaService : ILogSistemaService
    {
        private readonly ILogSistemaSqlRepository _logSistemaRepository;
        public LogSistemaService(ILogSistemaSqlRepository logSistemaRepository)
        {
            _logSistemaRepository = logSistemaRepository;
        }
        public async Task Add(LogSistema log)
        {
            try
            {
                await _logSistemaRepository.Add(log);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AddByJob(LogSistema log)
        {
            try
            {
                _logSistemaRepository.AddByJob(log);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
