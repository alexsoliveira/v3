using AutoMapper;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities.Procuracoes;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer.Procuracoes;
using TGS.Cartorio.Domain.Services.OutorgadoProcuracoesPartesStrategy.Factories;

namespace TGS.Cartorio.Domain.Services.OutorgadoProcuracoesPartesStrategy
{
    public static class OutorgadoContext
    {
        public static async Task Resolve(Outorgados outorgado,
                                         IMapper mapper,
                                         IProcuracoesPartesSqlRepository procuracoesPartesSqlRepository,
                                         IUsuariosSqlRepository usuariosSqlRepository,
                                         IPessoasSqlRepository pessoasSqlRepository)
        {
            OutorgadoFactory outorgadoNovoFactory = new OutorgadoNovoFactory(mapper,
                                                                             procuracoesPartesSqlRepository,
                                                                             usuariosSqlRepository,
                                                                             pessoasSqlRepository);

            OutorgadoFactory outorgadoExistenteFactory = new OutorgadoExistenteFactory(mapper,
                                                                                       procuracoesPartesSqlRepository,
                                                                                       usuariosSqlRepository,
                                                                                       pessoasSqlRepository);

            OutorgadoFactory outorgadoNuloFactory = new OutorgadoNuloFactory();

            outorgadoNovoFactory.Proximo = outorgadoExistenteFactory;
            outorgadoExistenteFactory.Proximo = outorgadoNuloFactory;

            await outorgadoNovoFactory.Run(outorgado);
        }
    }
}
