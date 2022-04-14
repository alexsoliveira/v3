using AutoMapper;
using System;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Entities.Procuracoes;
using TGS.Cartorio.Domain.Enumerables;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer.Procuracoes;

namespace TGS.Cartorio.Domain.Services.OutorgadoProcuracoesPartesStrategy.Strategies
{
    public class OutorgadoNovo : OutorgadoBase
    {

        IProcuracoesPartesSqlRepository _procuracoesPartesSqlRepository;
        IUsuariosSqlRepository _usuarioSqlRepository;
        IPessoasSqlRepository _pessoaSqlRepository;
        public OutorgadoNovo(IProcuracoesPartesSqlRepository procuracoesPartesSqlRepository,
                             IUsuariosSqlRepository usuarioSqlRepository,
                             IPessoasSqlRepository pessoaSqlRepository,
                             IMapper mapper)
            : base(mapper)
        {
            _procuracoesPartesSqlRepository = procuracoesPartesSqlRepository;
            _usuarioSqlRepository = usuarioSqlRepository;
            _pessoaSqlRepository = pessoaSqlRepository;
            _mapper = mapper;
        }

        public override async Task Create(Outorgados outorgado)
        {
            try
            {
                var dataOperacao = DateTime.Now;

                var usuario = new Usuarios()
                {
                    NomeUsuario = outorgado.Nome,
                    Email = outorgado.Email,
                    FlagAtivo = true,
                    DataOperacao = dataOperacao
                };
                await _usuarioSqlRepository.Incluir(usuario);

                var pessoa = new Pessoas()
                {
                    IdTipoDocumento = outorgado.IdTipoDocumento,
                    Documento = outorgado.Documento,
                    IdUsuario = usuario.IdUsuario,
                    FlagAtivo = true,
                    DataOperacao = dataOperacao
                };

                await this._pessoaSqlRepository.Incluir(pessoa);

                usuario.IdPessoa = pessoa.IdPessoa;
                await this._usuarioSqlRepository.Atualizar(usuario);

                var procuracaoParte = base.ToDomain(outorgado, pessoa.IdPessoa);

                await _procuracoesPartesSqlRepository.Incluir(procuracaoParte);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
