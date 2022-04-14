using System;
using System.Collections.Generic;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Infrastructure.SqlServer.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace TGS.Cartorio.Infrastructure.SqlServer.Repositories
{
    public class UsuariosContatosSqlRepository : IUsuariosContatosSqlRepository
    {
        private readonly EFContext _context;
        private readonly int _tamanhoPagina;
        private DbSet<UsuariosContatos> _dbSet;

        public UsuariosContatosSqlRepository(EFContext context, IConfiguration configuration)
        {
            _context = context;
            _tamanhoPagina = configuration.GetValue("SqlServer:TamanhoPagina", 50);
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task Incluir(UsuariosContatos usuario)
        {
            _context.UsuariosContatos.Add(usuario);
            await _context.Commit();
        }

        public async Task Atualizar(UsuariosContatos usuario)
        {
            _context.UsuariosContatos.Update(usuario);
            await _context.Commit();
        }

        public async Task<List<UsuariosContatos>> BuscarTodos(Expression<Func<UsuariosContatos, bool>> func, int pagina)
        {
            return await _context.UsuariosContatos
                            .Where(func)
                            .Skip(pagina * _tamanhoPagina)
                            .Take(_tamanhoPagina)
                            .ToListAsync();
        }

        public async Task<List<UsuariosContatos>> BuscarTodosComNoLock(Expression<Func<UsuariosContatos, bool>> func, int pagina = 0)
        {
            return await _context.UsuariosContatos
                    .AsNoTracking()
                    .Where(func)
                    .Skip(pagina * _tamanhoPagina)
                    .Take(_tamanhoPagina)
                    .ToListAsync();
        }

        public void Dispose()
        {
            //_context?.Dispose();
        }

        public async Task<UsuariosContatos> Buscar(Expression<Func<UsuariosContatos, bool>> func)
        {
            return await _context.UsuariosContatos.FirstOrDefaultAsync(func);
        }

        public async Task<UsuariosContatos> BuscarId(int id)
        {
            return await _context.UsuariosContatos
                .Where(p => p.IdUsuario == id)
                .FirstOrDefaultAsync();
        }
      
    }
}