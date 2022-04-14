using AutoMapper;
using System;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities.Procuracoes;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer.Procuracoes;
using TGS.Cartorio.Domain.Interfaces.Services.Strategy;
using TGS.Cartorio.Domain.Services.ConcreteStrategy;

namespace TGS.Cartorio.Application.DTO
{
    public class OutorgantesDto
    {
        public long IdProcuracaoParte { get; set; }
        public long IdSolicitacao { get; set; }
        public long? IdPessoa { get; set; }
        public string Nome { get; set; }
        public int IdTipoDocumento { get; set; }
        public long Documento { get; set; }
        public int IdTipoProcuracaoParte { get; set; }
        public int IdProcuracaoParteEstado { get; set; }
        public string ConteudoPessoasFisicas { get; set; }
        public string ConteudoPessoasContatos { get; set; }
        public string JsonConteudo { get; set; }
        public string EnderecoEntrega { get; set; }
        public string Email { get; set; }
        public long IdUsuario { get; set; }                

        public async Task ValidarRegrasCriacaoOutorgante(
            long IdPessoaSolicitante,
            IProcuracoesPartesSqlRepository procuracoesPartesSqlRepository,
            IProcuracoesPartesEstadosSqlRepository procuracoesPartesEstadosSqlRepository,
            IUsuariosSqlRepository usuarioSqlRepository,
            IPessoasSqlRepository pessoaSqlRepository,
            IMapper mapper
            )
        {
            try
            {
                var outorgante = mapper.Map<Outorgante>(this);
                IRegrasOutorgantesStrategy strategy =
                    Context.Resolve(
                        procuracoesPartesSqlRepository,
                        usuarioSqlRepository,
                        pessoaSqlRepository,
                        procuracoesPartesEstadosSqlRepository,
                        mapper,
                        IdPessoaSolicitante,
                        outorgante);

                await strategy.CriaOutorgante(outorgante);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
    }
}
