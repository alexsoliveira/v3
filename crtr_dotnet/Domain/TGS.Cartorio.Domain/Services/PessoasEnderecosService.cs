using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Services;
using System.Linq;

namespace TGS.Cartorio.Domain.Services
{

    public class PessoasEnderecosService : IPessoasEnderecosService
    {
        private readonly IConfiguracoesService _configuracoesService;

        private readonly IPessoasEnderecosSqlRepository _pessoasEnderecosRepositorio;
        private static HttpClient _httpClient;
        private static HttpClient HttpClient => _httpClient ?? (_httpClient = new HttpClient());


        public PessoasEnderecosService(IPessoasEnderecosSqlRepository pessoasEnderecosRepositorio,
                                        IConfiguracoesService configuracoesService
            )
        {
            _pessoasEnderecosRepositorio = pessoasEnderecosRepositorio;
            _configuracoesService = configuracoesService;
        }

        public async Task Incluir(PessoasEnderecos pessoaendereco)
        {
            await _pessoasEnderecosRepositorio.Incluir(pessoaendereco);
        }
        public async Task Atualizar(PessoasEnderecos pessoaendereco)
        {
            pessoaendereco.DataOperacao = DateTime.Now;
            await _pessoasEnderecosRepositorio.Atualizar(pessoaendereco);
        }

        public async Task RemoverPorIdEndereco(long idEndereco)
        {
            await _pessoasEnderecosRepositorio.RemoverPorIdEndereco(idEndereco);
        }

        public async Task<PessoasEnderecos> BuscarId(long id)
        {
            return await _pessoasEnderecosRepositorio.BuscarId(id);
        }

        public async Task<int> CountByIdPessoa(long idPessoa)
        {
            return await _pessoasEnderecosRepositorio.CountByIdPessoa(idPessoa);
        }

        public async Task<List<PessoasEnderecos>> BuscarPorPessoa(long idPessoa)
        {
            return await _pessoasEnderecosRepositorio.BuscarPorPessoa(idPessoa);
        }

        public async Task<PessoasEnderecos> BuscarPorEndereco(long idEndereco)
        {
            return await _pessoasEnderecosRepositorio.BuscarPorEnderecoUnico(idEndereco);
        }

        public async Task<long?> BuscarPessoaPorEndereco(long idEndereco)
        {
            return await _pessoasEnderecosRepositorio.BuscarPessoaPorEndereco(idEndereco);
        }

        public async Task<List<PessoasEnderecos>> BuscarTodos(int pagina = 0)
        {
            return await _pessoasEnderecosRepositorio.BuscarTodos(u => true, pagina);

        }

        public async Task<List<PessoasEnderecos>> BuscarTodosComNoLock(int pagina = 0)
        {
            return await _pessoasEnderecosRepositorio.BuscarTodosComNoLock(u => true, pagina);
        }

        public async Task<object> ConsultarCep(long cep)
        {
            string _retorno = "";

            var _configuracao = (await _configuracoesService.BuscarTodos(p => p.Descricao == "UrlViaCep")).FirstOrDefault();

            if (_configuracao == null) throw new Exception("A configuração UrlViaCep está vazia.");

            HttpResponseMessage response = await HttpClient.GetAsync(_configuracao.Conteudo.Replace("{cep}", cep.ToString().PadLeft(8, '0')));

            if (response.IsSuccessStatusCode) _retorno = await response.Content.ReadAsStringAsync();

            return _retorno;
        }
    }
}

