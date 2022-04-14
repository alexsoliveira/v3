using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Domain.Services
{
    public class ConfiguracoesService : IConfiguracoesService
    {
        private readonly IConfiguracoesSqlRepository _configuracoesSqlRepository;

        public ConfiguracoesService(IConfiguracoesSqlRepository configuracoesSqlRepository)
        {
            _configuracoesSqlRepository = configuracoesSqlRepository;
        }

        public async Task<List<Configuracoes>> BuscarTodos(int pagina = 0)
        {
            try
            {
                return await _configuracoesSqlRepository.BuscarTodos(pagina);
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
                return await _configuracoesSqlRepository.BuscarTodos(func, pagina);
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
                return await _configuracoesSqlRepository.BuscarTodosComNoLock(pagina);
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
                return await _configuracoesSqlRepository.BuscarPorDescricao(func);
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
    }
}
