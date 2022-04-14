using AutoMapper;
using System;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities.Procuracoes;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer.Procuracoes;
using TGS.Cartorio.Domain.Interfaces.Services.Strategy;

namespace TGS.Cartorio.Domain.Services.ConcreteStrategy
{
    public class CriarOutorganteExisteNoSistema : CriarOutorganteBase, IRegrasOutorgantesStrategy
    {
        private readonly IUsuariosSqlRepository _usuariosSqlRepository;
        public CriarOutorganteExisteNoSistema(
            IProcuracoesPartesSqlRepository procuracoesPartesSqlRepository,
            IUsuariosSqlRepository usuariosSqlRepository,
            IProcuracoesPartesEstadosSqlRepository procuracoesPartesEstadosSqlRepository,
            IMapper mapper) 
            : base(procuracoesPartesSqlRepository, procuracoesPartesEstadosSqlRepository, mapper)
        {
            _usuariosSqlRepository = usuariosSqlRepository;
        }

        public async Task CriaOutorgante(Outorgante data)
        {
            try
            {
                var usuario = await _usuariosSqlRepository.Buscar(u => u.IdPessoa == data.IdPessoa.Value);
                if (usuario != null)
                    data.Email = usuario.Email;

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
