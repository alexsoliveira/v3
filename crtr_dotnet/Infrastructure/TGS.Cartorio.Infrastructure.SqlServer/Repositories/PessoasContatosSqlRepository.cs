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
    public class PessoasContatosSqlRepository : IPessoasContatosSqlRepository
    {
        private readonly EFContext _context;
        private readonly int _tamanhoPagina;

        public PessoasContatosSqlRepository(EFContext context, IConfiguration configuration)
        {
            _context = context;
            _tamanhoPagina = configuration.GetValue("SqlServer:TamanhoPagina", 50);
        }


        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public async Task Incluir(PessoasContatos pessoacontato)
        {
            _context.PessoasContatos.Add(pessoacontato);
            await _context.Commit();
        }
        public async Task Atualizar(PessoasContatos pessoacontato)
        {
            _context.PessoasContatos.Update(pessoacontato);
            await _context.Commit();
        }

        public void Dispose()
        {
            ////_context?.Dispose();
        }

        public async Task<List<PessoasContatos>> BuscarTodos(Expression<Func<PessoasContatos, bool>> func, int pagina = 0)
        {
            return await _context.PessoasContatos
                       .Where(func)
                       .Skip(pagina * _tamanhoPagina)
                       .Take(_tamanhoPagina)
                       .ToListAsync();
        }

        public async Task<List<PessoasContatos>> BuscarTodosComNoLock(Expression<Func<PessoasContatos, bool>> func, int pagina = 0)
        {
            return await _context.PessoasContatos
               .AsNoTracking()
               .Where(func)
               .Skip(pagina * _tamanhoPagina)
               .Take(_tamanhoPagina)
               .ToListAsync();
        }

        public async Task<PessoasContatos> BuscarId(long id)
        {
            return await _context.PessoasContatos
                .Where(p => p.IdPessoaContato == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PessoasContatos>> BuscarPorPessoa(long idPessoa)
        {
            return await _context.PessoasContatos
                                 .Include(p => p.IdContatoNavigation)
                                 .Where(p => p.IdPessoa == idPessoa)
                                 .ToListAsync();
        }
    }
}
