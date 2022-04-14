using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Application.AppServices
{
    public class ConfiguracoesAppService : IConfiguracoesAppService
    {
        private readonly IConfiguracoesService _configuracoesService;

        public ConfiguracoesAppService(IConfiguracoesService configuracoesService)
        {
            _configuracoesService = configuracoesService;
        }

        public async Task<List<Configuracoes>> BuscarTodos(int pagina)
        {
            try
            {
                return await _configuracoesService.BuscarTodos(p => true, pagina);
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public async Task<List<Configuracoes>> BuscarTodosComNoLock(int pagina)
        {
            try
            {
                return await _configuracoesService.BuscarTodosComNoLock(pagina);
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public async Task<List<Configuracoes>> BuscarTodos(Expression<Func<Configuracoes, bool>> func, int pagina)
        {
            try
            {
                return await _configuracoesService.BuscarTodos(func, pagina);
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
                return await _configuracoesService.BuscarPorDescricao(func);
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
    }
}
