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
    public class ProdutosSqlRepository : IProdutosSqlRepository
    {
        private readonly EFContext _context;
        private readonly int _tamanhoPagina;

        public ProdutosSqlRepository(EFContext context, IConfiguration configuration)
        {
            _context = context;
            _tamanhoPagina = configuration.GetValue("SqlServer:TamanhoPagina", 50);
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task Incluir(Produtos produto)
        {
            _context.Produtos.Add(produto);
            await _context.Commit();
        }
        public async Task Atualizar(Produtos produto)
        {
            _context.Produtos.Update(produto);
            await _context.Commit();
        }


        public async Task<List<Produtos>> BuscarTodos(Expression<Func<Produtos, bool>> func, int pagina = 0)
        {
            return await _context.Produtos
                            .Where(func)
                            .Skip(pagina * _tamanhoPagina)
                            .Take(_tamanhoPagina)
                            .Include(p => p.ProdutosImagens)
                            .Include(p => p.ProdutosDocumentos)
                            .Include(p => p.ProdutosModalidades)
                                .ThenInclude(x => x.IdModalidadeNavigation)
                            .ToListAsync();
        }

        public async Task<List<ProdutosCategoriasPc>> BuscarDadosVitrine()
        {
            try
            {
                return await _context.ProdutosCategoriasPc
                            .Include(p => p.Produtos)
                                .ThenInclude(p => p.ProdutosImagens)
                            .Include(p => p.Produtos)
                                .ThenInclude(p => p.ProdutosModalidades)
                            .ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<Produtos>> BuscarTodosComNoLock(Expression<Func<Produtos, bool>> func, int pagina = 0)
        {
            return await _context.Produtos
                   .AsNoTracking()
                   .Where(func)
                   .Skip(pagina * _tamanhoPagina)
                   .Take(_tamanhoPagina)
                   .Include(p => p.ProdutosImagens)
                   .Include(p => p.ProdutosDocumentos)
                   .Include(p => p.ProdutosModalidades)
                   .ToListAsync();
        }

        public void Dispose()
        {
            //_context?.Dispose();
        }
        public async Task<Produtos> BuscarId(int id)
        {
            return await _context.Produtos
                .Where(p => p.IdProduto == id)
                 .Include(p => p.ProdutosImagens)
                 .Include(p => p.IdProdutoCategoriaNavigation)
                 .Include(p => p.ProdutosModalidades)
                    .ThenInclude(x => x.IdModalidadeNavigation)
                .FirstOrDefaultAsync();
        }
    }
}