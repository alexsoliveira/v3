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
    public class UsuariosSqlRepository : IUsuariosSqlRepository
    {
        private readonly EFContext _context;
        private readonly int _tamanhoPagina;

        public UsuariosSqlRepository(EFContext context, IConfiguration configuration)
        {
            _context = context;
            _tamanhoPagina = configuration.GetValue("SqlServer:TamanhoPagina", 50);
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task Incluir(Usuarios usuario)
        {
            try
            {
                usuario.DataOperacao = DateTime.Now;
                _context.Usuarios.Add(usuario);
                await UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public async Task Atualizar(Usuarios usuario)
        {
            try
            {
                _context.Usuarios.Update(usuario);
                await _context.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public async Task<List<Usuarios>> BuscarTodos(Expression<Func<Usuarios, bool>> func, int pagina)
        {
            try
            {
                return await _context.Usuarios
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

        public async Task<List<Usuarios>> BuscarTodosComNoLock(Expression<Func<Usuarios, bool>> func, int pagina = 0)
        {
            try
            {
                return await _context.Usuarios
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
                ////_context?.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public async Task<Usuarios> Buscar(Expression<Func<Usuarios, bool>> func)
        {
            try
            {
                return await _context.Usuarios.FirstOrDefaultAsync(func);
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public async Task<Usuarios> BuscarId(long id)
        {
            try
            {
                return await _context.Usuarios
                .Where(p => p.IdUsuario == id)
                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public async Task<Usuarios> BuscarPorIdPessoa(long idPessoa)
        {
            try
            {
                return await _context.Usuarios
                                     .AsNoTracking()
                                     .Include(p => p.IdPessoaNavigation)
                                     .FirstOrDefaultAsync(p => p.IdPessoa == idPessoa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Usuarios> BuscarEmail(string email)
        {
            try
            {
                return await _context.Usuarios
                .Where(p => p.Email == email)
                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}