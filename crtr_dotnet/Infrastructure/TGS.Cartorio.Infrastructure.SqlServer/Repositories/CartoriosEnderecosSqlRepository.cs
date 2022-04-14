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
    public class CartoriosEnderecosSqlRepository : ICartoriosEnderecosSqlRepository
    {
        private readonly EFContext _context;
        private readonly int _tamanhoPagina;

        public CartoriosEnderecosSqlRepository(EFContext context, IConfiguration configuration)
        {
            _context = context;     
            _tamanhoPagina = configuration.GetValue("SqlServer:TamanhoPagina", 50);
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task Incluir(CartoriosEnderecos cartorioenderecos)
        {
            _context.CartoriosEnderecos.Add(cartorioenderecos);
            await _context.Commit();
        }
        public async Task Atualizar(CartoriosEnderecos cartorioenderecos)
        {
            _context.CartoriosEnderecos.Update(cartorioenderecos);
            await _context.Commit();
        }

        public async Task<List<CartoriosEnderecos>> BuscarTodos(Expression<Func<CartoriosEnderecos, bool>> func, int pagina = 0)
        {
            return await _context.CartoriosEnderecos
                            .Where(func)
                            .Skip(pagina * _tamanhoPagina)
                            .Take(_tamanhoPagina)
                            .ToListAsync();
        }

        public async Task<List<CartoriosEnderecos>> BuscarTodosComNoLock(Expression<Func<CartoriosEnderecos, bool>> func, int pagina = 0)
        {
            return await _context.CartoriosEnderecos
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

        public async  Task<CartoriosEnderecos> BuscarId(int id)
        {
            return await _context.CartoriosEnderecos
                .Where(p => p.IdCartorioEndereco == id)
                .FirstOrDefaultAsync();
        }
    }
}