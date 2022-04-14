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
    public class PessoasFisicasSqlRepository : IPessoasFisicasSqlRepository
    {
        private readonly EFContext _context;
        private readonly int _tamanhoPagina;

        public PessoasFisicasSqlRepository(EFContext context, IConfiguration configuration)
        {
            _context = context;
            _tamanhoPagina = configuration.GetValue("SqlServer:TamanhoPagina", 50);
        }

        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public async Task Incluir(PessoasFisicas pessoafisica)
        {
            _context.PessoasFisicas.Add(pessoafisica);
            await _context.Commit();
        }
        public async Task Atualizar(PessoasFisicas pessoafisica)
        {
            _context.PessoasFisicas.Update(pessoafisica);
            await _context.Commit();
        }

        public void Dispose()
        {
            ////_context?.Dispose();
        }

        public async Task<List<PessoasFisicas>> BuscarTodos(Expression<Func<PessoasFisicas, bool>> func, int pagina = 0)
        {
            return await _context.PessoasFisicas
                       .Where(func)
                       .Skip(pagina * _tamanhoPagina)
                       .Take(_tamanhoPagina)
                       .ToListAsync();
        }

        public async Task<List<PessoasFisicas>> BuscarTodosComNoLock(Expression<Func<PessoasFisicas, bool>> func, int pagina = 0)
        {
            return await _context.PessoasFisicas
               .AsNoTracking()
               .Where(func)
               .Skip(pagina * _tamanhoPagina)
               .Take(_tamanhoPagina)
               .ToListAsync();
        }

        public async Task<PessoasFisicas> BuscarId(long id)
        {
            return await _context.PessoasFisicas
                .Where(p => p.IdPessoaFisica == id)
                .FirstOrDefaultAsync();
        }

        public async Task<PessoasFisicas> BuscarPorIdPessoa(long idPessoa)
        {
            return await _context.PessoasFisicas
                .Where(p => p.IdPessoa == idPessoa)
                .FirstOrDefaultAsync();
        }
    }
}
