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
    public class PessoasJuridicasSqlRepository : IPessoasJuridicasSqlRepository
    {
        private readonly EFContext _context;
        private readonly int _tamanhoPagina;

        public PessoasJuridicasSqlRepository(EFContext context, IConfiguration configuration)
        {
            _context = context;
            _tamanhoPagina = configuration.GetValue("SqlServer:TamanhoPagina", 50);
        }

        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public async Task Incluir(PessoasJuridicas pessoajuridica)
        {
            _context.PessoasJuridicas.Add(pessoajuridica);
            await _context.Commit();
        }

        public async Task Atualizar(PessoasJuridicas pessoajuridica)
        {
            _context.PessoasJuridicas.Update(pessoajuridica);
            await _context.Commit();
        }


        public void Dispose()
        {
            ////_context?.Dispose();
        }

        public async Task<List<PessoasJuridicas>> BuscarTodos(Expression<Func<PessoasJuridicas, bool>> func, int pagina = 0)
        {
            return await _context.PessoasJuridicas
                       .Where(func)
                       .Skip(pagina * _tamanhoPagina)
                       .Take(_tamanhoPagina)
                       .ToListAsync();
        }

        public async Task<List<PessoasJuridicas>> BuscarTodosComNoLock(Expression<Func<PessoasJuridicas, bool>> func, int pagina = 0)
        {
            return await _context.PessoasJuridicas
               .AsNoTracking()
               .Where(func)
               .Skip(pagina * _tamanhoPagina)
               .Take(_tamanhoPagina)
               .ToListAsync();
        }

        public async Task<PessoasJuridicas> BuscarId(long id)
        {
            return await _context.PessoasJuridicas
                .Where(p => p.IdPessoaJuridica == id)
                .FirstOrDefaultAsync();
        }
    }
}
