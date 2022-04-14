using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Application.DTO;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Application.AppServices
{
    public class PessoasEnderecosAppService : IPessoasEnderecosAppService
    {
        private readonly IPessoasEnderecosService _pessoaEnderecoService;
        private readonly IEnderecosService _enderecoService;
        private readonly ILogSistemaAppService _logSistemaAppService;
        public PessoasEnderecosAppService(IPessoasEnderecosService pessoaEnderecoService, IEnderecosService enderecoService, ILogSistemaAppService logSistemaAppService)
        {
            _pessoaEnderecoService = pessoaEnderecoService;
            _enderecoService = enderecoService;
            _logSistemaAppService = logSistemaAppService;
        }

        public async Task Incluir(PessoasEnderecos pessoaendereco)
        {
            await _pessoaEnderecoService.Incluir(pessoaendereco);
        }
        public async Task Atualizar(PessoasEnderecos pessoaendereco)
        {
            await _pessoaEnderecoService.Atualizar(pessoaendereco);
        }
        public async Task RemoverPorIdEndereco(long idEndereco)
        {
            await _pessoaEnderecoService.RemoverPorIdEndereco(idEndereco);
        }
        public async Task<PessoasEnderecos> BuscarId(long id)
        {
            return await _pessoaEnderecoService.BuscarId(id);
        }


        public async Task<int> CountByIdPessoa(long idPessoa)
        {
            return await _pessoaEnderecoService.CountByIdPessoa(idPessoa);
        }

        public async Task<List<PessoasEnderecos>> BuscarPorPessoa(long idPessoa)
        {
            try
            {
                return await _pessoaEnderecoService.BuscarPorPessoa(idPessoa);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_PessoasEnderecosAppService_BuscarPorPessoa, new
                {
                    IdPessoa = idPessoa
                }, ex);

                throw;
            }
        }

        public async Task<PessoasEnderecos> BuscarPorEndereco(long idEndereco)
        {
            try
            {
                return await _pessoaEnderecoService.BuscarPorEndereco(idEndereco);
            }
            catch (Exception ex)
            {
                await _logSistemaAppService.Add(CodLogSistema.Erro_PessoasEnderecosAppService_BuscarPorEndereco, new
                {
                    IdEndereco = idEndereco
                }, ex);

                throw;
            }
        }

        public async Task AtualizarEnderecoPrincipal(long idEnderecoPrincipal)
        {
            try
            {
                long? idPessoa = await _pessoaEnderecoService.BuscarPessoaPorEndereco(idEnderecoPrincipal);
                if (idPessoa.HasValue)
                {
                    var pessoasEnderecos = await _pessoaEnderecoService.BuscarPorPessoa(idPessoa.Value);
                    foreach (var pessoaEndereco in pessoasEnderecos)
                    {
                        Enderecos endereco = pessoaEndereco.IdEnderecoNavigation;
                        bool existEndereco = endereco != null;
                        bool isEnderecoPrincipal = pessoaEndereco.IdEndereco == idEnderecoPrincipal;
                        
                        //endereço principal e flag ativo igual a nulo ou false
                        if (existEndereco
                            && isEnderecoPrincipal
                            && (!endereco.FlagAtivo.HasValue || (endereco.FlagAtivo.HasValue && !endereco.FlagAtivo.Value)))
                        {
                            endereco.FlagAtivo = true;
                            await _enderecoService.Atualizar(endereco);
                        }
                        
                        else if (existEndereco && !isEnderecoPrincipal && endereco.FlagAtivo.HasValue && endereco.FlagAtivo.Value)
                        {
                            endereco.FlagAtivo = false;
                            await _enderecoService.Atualizar(endereco);
                        }
                    }

                }
                else
                    throw new Exception("Endereço não localizado!");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<PessoasEnderecos>> BuscarTodos(int pagina = 0)
        {
            return await _pessoaEnderecoService.BuscarTodos(pagina);
        }

        public async Task<List<PessoasEnderecos>> BuscarTodosComNoLock(int pagina = 0)
        {
            return await _pessoaEnderecoService.BuscarTodosComNoLock(pagina);
        }

        public async  Task<object> ConsultarCep(long cep)
        {
            return await _pessoaEnderecoService.ConsultarCep(cep);
        }
    }
}
