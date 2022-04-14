using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Services;
using System.Linq.Expressions;
using System;
using TGS.Cartorio.Application.DTO;

namespace TGS.Cartorio.Application.AppServices
{
    public class PessoasAppService : IPessoasAppService
    {
        private readonly IPessoasService _pessoaService;
        private readonly ILogSistemaAppService _logSistemaAppService;
        public PessoasAppService(IPessoasService pessoaService, ILogSistemaAppService logSistemaAppService)
        {
            _pessoaService = pessoaService;
            _logSistemaAppService = logSistemaAppService;
        }

        public async Task Incluir(Pessoas pessoa)
        {
            // TODO: Criar validações


            // TODO: Verificar se usuário existe na base
            // usuario.IdUsuario = _usuarioService.Incluir(usuario);
            await _pessoaService.Incluir(pessoa);
        }
        public async Task<Pessoas> BuscarId(long id)
        {
            // TODO: Obter todos os usuários com paginação
            return await _pessoaService.BuscarId(id);
        }

        public async Task<Pessoas> BuscarPorIdCompleto(long id)
        {
            try
            {
                return await _pessoaService.BuscarPorIdCompleto(id);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_PessoasAppService_BuscarPorIdCompleto, new
                {
                    IdPessoaSolicitante = id
                }, ex);
                throw;
            }
        }

        public async Task<List<Pessoas>> BuscarTodos(int pagina = 0)
        {
            // TODO: Obter todos os usuários com paginação
            return await _pessoaService.BuscarTodos(pagina);
        }

        public async Task<List<Pessoas>> BuscarTodosComNoLock(int pagina = 0)
        {
            // TODO: Obter todos os usuários com paginação sem no lock
            return await _pessoaService.BuscarTodosComNoLock(pagina);
        }


        public async Task Atualizar(Pessoas pessoa)
        {
            await _pessoaService.Atualizar(pessoa);
        }

        public async  Task<List<Pessoas>> BuscarTodosComNoLock(Expression<Func<Pessoas, bool>> func, int pagina)
        {
            return await _pessoaService.BuscarTodosComNoLock(func, pagina);
        }

        public async Task<long?> PessoaExiste(int idTipoDocumento, long documento)
        {
            try
            {
                return await _pessoaService.PessoaExiste(idTipoDocumento, documento);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}