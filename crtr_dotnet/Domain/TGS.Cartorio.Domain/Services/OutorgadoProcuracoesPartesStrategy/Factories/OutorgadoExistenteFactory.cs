using AutoMapper;
using TGS.Cartorio.Domain.Entities.Procuracoes;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer.Procuracoes;
using TGS.Cartorio.Domain.Services.OutorgadoProcuracoesPartesStrategy.Strategies;

namespace TGS.Cartorio.Domain.Services.OutorgadoProcuracoesPartesStrategy.Factories
{
    public class OutorgadoExistenteFactory : OutorgadoFactory
    {
        private readonly IProcuracoesPartesSqlRepository _procuracoesPartesSqlRepository;
        private readonly IUsuariosSqlRepository _usuariosPartesSqlRepository;
        private readonly IPessoasSqlRepository _pessoasSqlRepository;
        public OutorgadoExistenteFactory(IMapper mapper, 
                                         IProcuracoesPartesSqlRepository procuracoesPartesSqlRepository,
                                         IUsuariosSqlRepository usuariosPartesSqlRepository,
                                         IPessoasSqlRepository pessoasSqlRepository)
            : base(mapper, procuracoesPartesSqlRepository)
        {
            _procuracoesPartesSqlRepository = procuracoesPartesSqlRepository;
            _usuariosPartesSqlRepository = usuariosPartesSqlRepository;
            _pessoasSqlRepository = pessoasSqlRepository;
        }

        protected override OutorgadoBase _strategy => new OutorgadoExistente(_procuracoesPartesSqlRepository,
                                                                             _usuariosPartesSqlRepository,
                                                                             _mapper);

        public override bool IsElegible(Outorgados outorgado)
        {
            return outorgado.IdPessoa != null;
        }
    }
}
