using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Infrastructure.SqlServer.Context;

namespace TGS.Cartorio.Infrastructure.SqlServer.Repositories
{
    public class ConfiguracoesSqlRepository : IConfiguracoesSqlRepository
    {
        private readonly EFContext _context;
        private readonly int _tamanhoPagina;

        public ConfiguracoesSqlRepository(EFContext context, IConfiguration configuration)
        {
            _context = context;
            _tamanhoPagina = configuration.GetValue("SqlServer:TamanhoPagina", 50);
        }

        public async Task<List<Configuracoes>> BuscarTodos(int pagina = 0)
        {
            try
            {
                return await _context.Configuracoes
                     .Skip(pagina * _tamanhoPagina)
                     .Take(_tamanhoPagina)
                     .ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public async Task<List<Configuracoes>> BuscarTodos(Expression<Func<Configuracoes, bool>> func, int pagina = 0)
        {
            try
            {
                return await _context.Configuracoes
                     .Where(func)
                     .Skip(pagina * _tamanhoPagina)
                     .Take(_tamanhoPagina)
                     .ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public async Task<List<Configuracoes>> BuscarTodosComNoLock(int pagina = 0)
        {
            try
            {
                return await _context.Configuracoes
               .AsNoTracking()
               .Skip(pagina * _tamanhoPagina)
               .Take(_tamanhoPagina)
               .ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public async Task<Configuracoes> BuscarPorDescricao(Expression<Func<Configuracoes, bool>> func)
        {
            try
            {
                return await _context.Configuracoes
                     .Where(func)
                     .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
    }
}