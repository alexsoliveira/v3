using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Application.AppServices
{
    public class ProdutosModalidadesAppService : IProdutosModalidadesAppService
    {
        private readonly IProdutosModalidadesService _produtomodalidadesservice;

        public ProdutosModalidadesAppService(IProdutosModalidadesService produtomodalidadesservice)
        {
            _produtomodalidadesservice = produtomodalidadesservice;
        }


        public async Task Incluir(ProdutosModalidades produtosmodalidades)
        {
            await _produtomodalidadesservice.Incluir(produtosmodalidades);
        }

        public async Task<List<ProdutosModalidades>> BuscarTodos(Expression<Func<ProdutosModalidades, bool>> func, int pagina)
        {
            return await _produtomodalidadesservice.BuscarTodos(func,pagina);
        }

        public async Task<List<ProdutosModalidades>> BuscarTodosComNoLock(Expression<Func<ProdutosModalidades, bool>> func, int pagina)
        {
            return await _produtomodalidadesservice.BuscarTodosComNoLock(func, pagina);
        }

        public async Task<ProdutosModalidades> BuscarId(int id)
        {
            return await _produtomodalidadesservice.BuscarId(id);
        }

        public async  Task Atualizar(ProdutosModalidades produtosmodalidades)
        {
            await _produtomodalidadesservice.Atualizar(produtosmodalidades);
        }
    }
}
