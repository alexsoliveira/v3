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
    public class PessoasEnderecosSqlRepository : IPessoasEnderecosSqlRepository
    {
        private readonly EFContext _context;
        private readonly int _tamanhoPagina;

        public PessoasEnderecosSqlRepository(EFContext context, IConfiguration configuration)
        {
            _context = context;
            _tamanhoPagina = configuration.GetValue("SqlServer:TamanhoPagina", 50);
        }

        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public async Task Incluir(PessoasEnderecos pessoaendereco)
        {
            _context.PessoasEnderecos.Add(pessoaendereco);
            await _context.Commit();
        }
        public async Task Atualizar(PessoasEnderecos pessoaendereco)
        {
            _context.PessoasEnderecos.Update(pessoaendereco);
            await _context.Commit();
        }

        public async Task<int> CountByIdPessoa(long idPessoa)
        {
            return await _context.PessoasEnderecos.CountAsync(p => p.IdPessoa == idPessoa);
        }

        public async Task RemoverPorIdEndereco(long idEndereco)
        {
            var pessoaEndereco = await _context.PessoasEnderecos.FirstOrDefaultAsync(p => p.IdEndereco == idEndereco);
            if (pessoaEndereco != null)
            {
                _context.Remove(pessoaEndereco);
                await _context.Commit();
            }
        }

        public void Dispose()
        {
            ////_context?.Dispose();
        }

        public async Task<List<PessoasEnderecos>> BuscarTodos(Expression<Func<PessoasEnderecos, bool>> func, int pagina = 0)
        {
            return await _context.PessoasEnderecos
                       .Where(func)
                       .Skip(pagina * _tamanhoPagina)
                       .Take(_tamanhoPagina)
                       .ToListAsync();
        }

        public async Task<List<PessoasEnderecos>> BuscarTodosComNoLock(Expression<Func<PessoasEnderecos, bool>> func, int pagina = 0)
        {
            return await _context.PessoasEnderecos
               .AsNoTracking()
               .Where(func)
               .Skip(pagina * _tamanhoPagina)
               .Take(_tamanhoPagina)
               .ToListAsync();
        }

        public async Task<PessoasEnderecos> BuscarId(long id)
        {
            return await _context.PessoasEnderecos
                .Where(p => p.IdPessoaEndereco == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<PessoasEnderecos>> BuscarPorPessoa(long idPessoa)
        {
            return await _context.PessoasEnderecos
                .AsNoTracking()
                .Include(p => p.IdEnderecoNavigation)
                .Where(p => p.IdPessoa == idPessoa)
                .ToListAsync();
        }

        public async Task<PessoasEnderecos> BuscarPorEnderecoUnico(long idEndereco)
        {
            return await Task.Run(() =>
            {
                return _context.PessoasEnderecos
                    .AsNoTracking()
                    .Include(p => p.IdEnderecoNavigation)
                    .FirstOrDefault(p => p.IdEndereco == idEndereco);
            });
        }

        public async Task<List<PessoasEnderecos>> BuscarPorEndereco(long idEndereco)
        {
            return await _context.PessoasEnderecos.Where(p => p.IdEndereco == idEndereco).ToListAsync();
        }

        public async Task<long?> BuscarPessoaPorEndereco(long idEndereco)
        {
            return (await _context.PessoasEnderecos.AsNoTracking().FirstOrDefaultAsync(p => p.IdEndereco == idEndereco))?.IdPessoa;
        }
    }
}
