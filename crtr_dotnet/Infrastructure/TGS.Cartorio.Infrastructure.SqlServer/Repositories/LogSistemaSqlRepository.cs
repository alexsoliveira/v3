using Newtonsoft.Json;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Infrastructure.SqlServer.Context;

namespace TGS.Cartorio.Infrastructure.SqlServer.Repositories
{
    public class LogSistemaSqlRepository : ILogSistemaSqlRepository
    {
        private readonly EFContext _context;
        public LogSistemaSqlRepository(EFContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task Add(LogSistema log)
        {
            try
            {
                log.JsonConteudo = JsonConvert.SerializeObject(log);
                log.JsonConteudo = log.JsonConteudo.Replace("\\", "")
                                   .Replace(System.Environment.NewLine, "")
                                   .Replace(@"\n", "")
                                   .Replace(@"\r", "")
                                   .Replace(@"\", "")
                                   .Replace(@"\\", "")
                                   .Replace("\"{", "{")
                                   .Replace("}\"", "}");
                _context.Add(log);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddByJob(LogSistema log)
        {
            try
            {
                log.JsonConteudo = JsonConvert.SerializeObject(log);
                _context.Add(log);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            //_context.Dispose();
        }
    }
}