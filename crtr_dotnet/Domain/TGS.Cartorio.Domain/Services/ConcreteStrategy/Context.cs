using AutoMapper;
using System;
using TGS.Cartorio.Domain.Entities.Procuracoes;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer.Procuracoes;
using TGS.Cartorio.Domain.Interfaces.Services.Strategy;

namespace TGS.Cartorio.Domain.Services.ConcreteStrategy
{
    public class Context
    {        
        public static IRegrasOutorgantesStrategy Resolve(
            IProcuracoesPartesSqlRepository procuracoesPartesSqlRepository,
            IUsuariosSqlRepository usuarioSqlRepository,
            IPessoasSqlRepository pessoaSqlRepository,
            IProcuracoesPartesEstadosSqlRepository procuracoesPartesEstadosSqlRepository,
            IMapper mapper,
            long idPessoaSolicitante, 
            Outorgante outorgante)
        {
            try
            {
                if (!outorgante.IdPessoa.HasValue)
                {
                    return new CriarOutorganteNaoExisteNoSistema(
                        procuracoesPartesSqlRepository,
                        procuracoesPartesEstadosSqlRepository,
                        usuarioSqlRepository,
                        pessoaSqlRepository,
                        mapper);
                }
                else if (idPessoaSolicitante == outorgante.IdPessoa)
                {
                    return new CriarOutorganteSolicitante(
                        procuracoesPartesSqlRepository,
                        procuracoesPartesEstadosSqlRepository,
                        mapper);
                }
                else
                {
                    return new CriarOutorganteExisteNoSistema(
                        procuracoesPartesSqlRepository,
                        usuarioSqlRepository,
                        procuracoesPartesEstadosSqlRepository,
                        mapper);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }      
    }
}
