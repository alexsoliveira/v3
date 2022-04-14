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
    public class CartoriosContatosSqlRepository : ICartoriosContatosSqlRepository
    {
        private readonly EFContext _context;
        private readonly int _tamanhoPagina;

        public CartoriosContatosSqlRepository(EFContext context, IConfiguration configuration)
        {
            _context = context;     
            _tamanhoPagina = configuration.GetValue("SqlServer:TamanhoPagina", 50);
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task Incluir(CartoriosContatos cartoriocontatos)
        {
            _context.CartoriosContatos.Add(cartoriocontatos);
            await _context.Commit();
        }

        public async Task Atualizar(CartoriosContatos cartoriocontatos)
        {
            _context.CartoriosContatos.Update(cartoriocontatos);
            await _context.Commit();
        }

        public async Task<List<CartoriosContatos>> BuscarTodos(Expression<Func<CartoriosContatos, bool>> func, int pagina = 0)
        {
            return await _context.CartoriosContatos
                            .Where(func)
                            .Skip(pagina * _tamanhoPagina)
                            .Take(_tamanhoPagina)
                            .ToListAsync();
        }

        public async Task<List<CartoriosContatos>> BuscarTodosComNoLock(Expression<Func<CartoriosContatos, bool>> func, int pagina = 0)
        {
            return await _context.CartoriosContatos
                   .AsNoTracking()
                   .Where(func)
                   .Skip(pagina * _tamanhoPagina)
                   .Take(_tamanhoPagina)
                   .ToListAsync();
        }

        public void Dispose()
        {
            ////_context?.Dispose();
        }

        public async Task<CartoriosContatos> BuscarId(int id)
        {
            return await _context.CartoriosContatos
                .Where(p => p.IdCartorio == id)
                .FirstOrDefaultAsync();
        }
    }
}