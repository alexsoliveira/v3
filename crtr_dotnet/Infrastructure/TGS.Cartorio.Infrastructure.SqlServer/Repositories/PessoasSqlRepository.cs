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
    public class PessoasSqlRepository : IPessoasSqlRepository
    {
        private readonly EFContext _context;
        private readonly int _tamanhoPagina;

        public PessoasSqlRepository(EFContext context, IConfiguration configuration)
        {
            _context = context;
            _tamanhoPagina = configuration.GetValue("SqlServer:TamanhoPagina", 50);
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task Incluir(Pessoas pessoa)
        {
            try
            {
                pessoa.DataOperacao = DateTime.Now;
                _context.Pessoas.Add(pessoa);
                await UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
        

        public async Task<List<Pessoas>> BuscarTodos(Expression<Func<Pessoas, bool>> func, int pagina = 0)
        {
            try
            {
                return await _context.Pessoas
                             .Where(func)
                             .Skip(pagina * _tamanhoPagina)
                             .Take(_tamanhoPagina)
                             .ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public async Task<List<Pessoas>> BuscarTodosComNoLock(Expression<Func<Pessoas, bool>> func, int pagina = 0)
        {
            try
            {
                return await _context.Pessoas
                   .AsNoTracking()
                   .Where(func)
                   .Skip(pagina * _tamanhoPagina)
                   .Take(_tamanhoPagina)
                   .ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public void Dispose()
        {
            try
            {
                //_context?.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public async Task Atualizar(Pessoas pessoa)
        {
            try
            {
                //_context.Pessoas.Attach(pessoa);
                //_context.Entry(pessoa).Property(p => p.FlagAtivo).IsModified = true;

                _context.Pessoas.Update(pessoa);
                await _context.Commit();
                //await _context.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public async Task Remover(Pessoas pessoa)
        {
            try
            {
                _context.Pessoas.Remove(pessoa).State = EntityState.Deleted;
                await _context.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Pessoas> BuscarId(long id)
        {
            try
            {
                return await _context.Pessoas.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public async Task<Pessoas> BuscarPorIdCompleto(long id)
        {
            try
            {
                return await _context.Pessoas
                    .AsNoTracking()
                    .Include(x => x.PessoasContatos)
                        .ThenInclude(x => x.IdContatoNavigation)
                    .FirstOrDefaultAsync(p => p.IdPessoa == id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<long?> ExisteAsync(int idTipoDocumento, long documento)
        {
            try
            {
                var pessoa = await _context.Pessoas.FirstOrDefaultAsync(p => p.IdTipoDocumento == idTipoDocumento && p.Documento == documento);
                return pessoa?.IdPessoa;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}