using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Domain.Services
{
    public class ProdutosModalidadesService : IProdutosModalidadesService
    {
        private readonly IProdutosModalidadesSqlRepository _produtosModalidadesRepository;

        public ProdutosModalidadesService(IProdutosModalidadesSqlRepository produtosModalidadesRepository)
        {
            _produtosModalidadesRepository = produtosModalidadesRepository;
        }

        public async Task Incluir(ProdutosModalidades produtosmodalidades)
        {
            await _produtosModalidadesRepository.Incluir(produtosmodalidades);
        }

        public async Task<List<ProdutosModalidades>> BuscarTodos(Expression<Func<ProdutosModalidades, bool>> func, int pagina)
        {
            return await _produtosModalidadesRepository.BuscarTodos(u => true, pagina);
        }

        public async Task<List<ProdutosModalidades>> BuscarTodosComNoLock(Expression<Func<ProdutosModalidades, bool>> func, int pagina)
        {
            return await _produtosModalidadesRepository.BuscarTodosComNoLock(u => true, pagina);
        }

        public async Task<ProdutosModalidades> BuscarId(int id)
        {
            return await _produtosModalidadesRepository.BuscarId(id);
        }

        public async Task Atualizar(ProdutosModalidades produtosmodalidades)
        {
            produtosmodalidades.DataOperacao = DateTime.Now;
            await _produtosModalidadesRepository.Atualizar(produtosmodalidades);
        }

    }
}
