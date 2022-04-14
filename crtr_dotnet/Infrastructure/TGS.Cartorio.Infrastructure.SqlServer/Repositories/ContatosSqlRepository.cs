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
    public class ContatosSqlRepository : IContatosSqlRepository
    {
        private readonly EFContext _context;
        private readonly int _tamanhoPagina;
        public ContatosSqlRepository(EFContext context, IConfiguration configuration)
        {
            _context = context;
            _tamanhoPagina = configuration.GetValue("SqlServer:TamanhoPagina", 50);
        }

        public IUnitOfWork UnitOfWork => throw new NotImplementedException();
        public async Task Incluir(Contatos contato)
        {
            _context.Contatos.Add(contato);
            await _context.Commit();
        }
        public async Task Atualizar(Contatos contato)
        {
            _context.Contatos.Update(contato);
            await _context.Commit();
        }
        
        public async Task<List<Contatos>> BuscarTodos(Expression<Func<Contatos, bool>> func, int pagina = 0)
        {
            return await _context.Contatos
                            .Where(func)
                            .Skip(pagina * _tamanhoPagina)
                            .Take(_tamanhoPagina)
                            .ToListAsync();
        }

        public async Task<List<Contatos>> BuscarTodosComNoLock(Expression<Func<Contatos, bool>> func, int pagina = 0)
        {
            return await _context.Contatos
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

        public async Task<Contatos> BuscarId(int id)
        {
            return await _context.Contatos
                .Where(p => p.IdContato == id)
                .FirstOrDefaultAsync();
        }
    }
}
