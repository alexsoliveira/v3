using AutoMapper;
using System;
using TGS.Cartorio.Domain.Entities.Procuracoes;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer.Procuracoes;
using TGS.Cartorio.Domain.Services.OutorgadoProcuracoesPartesStrategy.Strategies;

namespace TGS.Cartorio.Domain.Services.OutorgadoProcuracoesPartesStrategy.Factories
{
    public class OutorgadoNovoFactory : OutorgadoFactory
    {
        IProcuracoesPartesSqlRepository _procuracoesPartesSqlRepository;
        IUsuariosSqlRepository _usuariosPartesSqlRepository;
        IPessoasSqlRepository _pessoasSqlRepository;
        public OutorgadoNovoFactory(IMapper mapper,
                                    IProcuracoesPartesSqlRepository procuracoesPartesSqlRepository,
                                    IUsuariosSqlRepository usuariosPartesSqlRepository,
                                    IPessoasSqlRepository pessoasSqlRepository)
            : base(mapper, procuracoesPartesSqlRepository)
        {
            _procuracoesPartesSqlRepository = procuracoesPartesSqlRepository;
            _usuariosPartesSqlRepository = usuariosPartesSqlRepository;
            _pessoasSqlRepository = pessoasSqlRepository;
        }

        protected override OutorgadoBase _strategy => new OutorgadoNovo(_procuracoesPartesSqlRepository,
                                                                        _usuariosPartesSqlRepository,
                                                                        _pessoasSqlRepository,
                                                                        _mapper);

        public override bool IsElegible(Outorgados outorgado)
        {
            try
            {
                return outorgado.IdPessoa == null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
