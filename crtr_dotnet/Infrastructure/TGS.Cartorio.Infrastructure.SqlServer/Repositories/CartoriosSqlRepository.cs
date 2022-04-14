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
    public class CartoriosSqlRepository : ICartoriosSqlRepository
    {
        private readonly EFContext _context;
        private readonly int _tamanhoPagina;

        public CartoriosSqlRepository(EFContext context, IConfiguration configuration)
        {
            _context = context;     
            _tamanhoPagina = configuration.GetValue("SqlServer:TamanhoPagina", 50);
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task Incluir(Cartorios cartorio)
        {
            _context.Cartorios.Add(cartorio);
            await _context.Commit();
        }

        public async Task Atualizar(Cartorios cartorio)
        {
            _context.Cartorios.Attach(cartorio);
            await _context.Commit();
        }

        public async Task<List<Cartorios>> BuscarTodos(Expression<Func<Cartorios, bool>> func, int pagina = 0)
        {
            return await _context.Cartorios
                .Include(c => c.CartoriosEnderecos)
                    .ThenInclude(c => c.IdEnderecoNavigation)
                .Where(func)
                .Skip(pagina * _tamanhoPagina)
                .Take(_tamanhoPagina)
                .ToListAsync();
        }

        public async Task<List<Cartorios>> BuscarTodosComNoLock(Expression<Func<Cartorios, bool>> func, int pagina = 0)
        {
            return await _context.Cartorios
                   .AsNoTracking()
                   .Where(func)
                   .Skip(pagina * _tamanhoPagina)
                   .Take(_tamanhoPagina)
                   .ToListAsync();
        }

        public Cartorios BuscarUltimoCartorioValido()
        {
            try
            {
                return _context.Cartorios
                            .AsNoTracking()
                            .Include(x => x.CartoriosEnderecos)
                                .ThenInclude(x => x.IdEnderecoNavigation)
                            .Include(x => x.IdPessoaNavigation)
                                .ThenInclude(x => x.PessoasContatos)
                                    .ThenInclude(x => x.IdContatoNavigation)
                            .Include(x => x.IdPessoaNavigation)
                                .ThenInclude(x => x.PessoasJuridicas)
                            .OrderByDescending(x => x.IdCartorio)
                            .FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Dispose()
        {
            ////_context?.Dispose();
        }

        public async Task<Cartorios> BuscarId(int id)
        {
            return await _context.Cartorios
                .Where(p => p.IdCartorio == id)
                .FirstOrDefaultAsync();
        }
    }
}