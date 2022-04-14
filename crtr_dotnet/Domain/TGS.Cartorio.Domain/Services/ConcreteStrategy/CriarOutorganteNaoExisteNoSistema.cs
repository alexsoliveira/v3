using AutoMapper;
using System;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Entities.Procuracoes;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer.Procuracoes;
using TGS.Cartorio.Domain.Interfaces.Services.Strategy;

namespace TGS.Cartorio.Domain.Services.ConcreteStrategy
{
    public class CriarOutorganteNaoExisteNoSistema : CriarOutorganteBase, IRegrasOutorgantesStrategy
    {        
        private readonly IUsuariosSqlRepository _usuarioSqlRepository;
        private readonly IPessoasSqlRepository _pessoaSqlRepository;
        private readonly IProcuracoesPartesSqlRepository _procuracoesPartesSqlRepository;
        public CriarOutorganteNaoExisteNoSistema(
            IProcuracoesPartesSqlRepository procuracoesPartesSqlRepository,
            IProcuracoesPartesEstadosSqlRepository procuracoesPartesEstadosSqlRepository,
            IUsuariosSqlRepository usuarioSqlRepository,
            IPessoasSqlRepository pessoaSqlRepository,
            IMapper mapper) 
            : base(procuracoesPartesSqlRepository, procuracoesPartesEstadosSqlRepository, mapper)
        {            
            _usuarioSqlRepository = usuarioSqlRepository;
            _pessoaSqlRepository = pessoaSqlRepository;
            _procuracoesPartesSqlRepository = procuracoesPartesSqlRepository;
        }

        public async Task CriaOutorgante(Outorgante data)
        {
            try
            {
                var dataOperacao = DateTime.Now;

                var usuario = new Usuarios()
                {
                    NomeUsuario = data.Nome,
                    Email = data.Email,
                    FlagAtivo = true,
                    DataOperacao = dataOperacao
                };
                await this._usuarioSqlRepository.Incluir(usuario);

                var pessoa = new Pessoas()
                {
                    IdTipoDocumento = data.IdTipoDocumento,
                    Documento = data.Documento,
                    IdUsuario = usuario.IdUsuario,
                    FlagAtivo = true,
                    DataOperacao = dataOperacao
                };
                await this._pessoaSqlRepository.Incluir(pessoa);

                usuario.IdPessoa = pessoa.IdPessoa;

                await this._usuarioSqlRepository.Atualizar(usuario);

                data.IdPessoa = pessoa.IdPessoa;
                var procuracaoParte = SetProcuracoesPartes(data);
                await _procuracoesPartesSqlRepository.Incluir(procuracaoParte);

                await base.IncluirProcuracaoParteEstadoInicial(procuracaoParte.IdProcuracaoParte);
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
    }
}
