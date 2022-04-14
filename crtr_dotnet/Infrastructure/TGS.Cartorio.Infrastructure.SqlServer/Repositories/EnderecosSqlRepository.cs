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
    public class EnderecosSqlRepository : IEnderecosSqlRepository
    {
        private readonly EFContext _context;
        private readonly int _tamanhoPagina;

        public EnderecosSqlRepository(EFContext context, IConfiguration configuration)
        {
            _context = context;
            _tamanhoPagina = configuration.GetValue("SqlServer:TamanhoPagina", 50);
        }

        IUnitOfWork ISqlRepository<Enderecos>.UnitOfWork => throw new NotImplementedException();

        public async Task Incluir(Enderecos endereco)
        {
            endereco.DataOperacao = DateTime.Now;
            _context.Enderecos.Add(endereco);
            await _context.Commit();
        }
        public async Task Atualizar(Enderecos endereco)
        {
            _context.Enderecos.Update(endereco);
            await _context.Commit();
        }

        public async Task<List<Enderecos>> BuscarTodos(Expression<Func<Enderecos, bool>> func, int pagina = 0)
        {
            return await _context.Enderecos
                            .Where(func)
                            .Skip(pagina * _tamanhoPagina)
                            .Take(_tamanhoPagina)
                            .ToListAsync();
        }

        public async Task<List<Enderecos>> BuscarTodosComNoLock(Expression<Func<Enderecos, bool>> func, int pagina = 0)
        {
            return await _context.Enderecos
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

        public async Task<Enderecos> BuscarId(int id)
        {
            return await _context.Enderecos
                .Where(p => p.IdEndereco == id)
                .FirstOrDefaultAsync();
        }

        public async Task Apagar(int Idendereco)
        {
            var endereco = await _context.Enderecos.Where(e => e.IdEndereco == Idendereco).FirstOrDefaultAsync();
            
            _context.Enderecos.Remove(endereco);
            await _context.Commit();
        }
    }
}
