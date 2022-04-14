using AutoMapper;
using System;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities.Procuracoes;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer.Procuracoes;
using TGS.Cartorio.Domain.Interfaces.Services.Strategy;

namespace TGS.Cartorio.Domain.Services.ConcreteStrategy
{
    public class CriarOutorganteSolicitante : CriarOutorganteBase, IRegrasOutorgantesStrategy
    {
        public CriarOutorganteSolicitante(
            IProcuracoesPartesSqlRepository procuracoesPartesSqlRepository,
            IProcuracoesPartesEstadosSqlRepository procuracoesPartesEstadosSqlRepository,
            IMapper mapper) 
            : base(procuracoesPartesSqlRepository, procuracoesPartesEstadosSqlRepository, mapper)
        { }

        public async Task CriaOutorgante(Outorgante data)
        {
            try
            {
                var procuracaoParte = SetProcuracoesPartes(data);
                await base.IncluirProcuracaoParte(procuracaoParte);

                await base.IncluirProcuracaoParteEstadoInicial(procuracaoParte.IdProcuracaoParte);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
    }
}
