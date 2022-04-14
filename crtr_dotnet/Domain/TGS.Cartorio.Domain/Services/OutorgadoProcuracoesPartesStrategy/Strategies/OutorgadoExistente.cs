using AutoMapper;
using System.Threading.Tasks;
using TGS.Cartorio.Domain.Entities.Procuracoes;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer;
using TGS.Cartorio.Domain.Interfaces.Repositories.SqlServer.Procuracoes;

namespace TGS.Cartorio.Domain.Services.OutorgadoProcuracoesPartesStrategy.Strategies
{
    public class OutorgadoExistente : OutorgadoBase
    {
        private readonly IProcuracoesPartesSqlRepository _procuracoesPartesSqlRepository;
        private readonly IUsuariosSqlRepository _usuariosRepository;
        public OutorgadoExistente(IProcuracoesPartesSqlRepository procuracoesPartesSqlRepository,
                                  IUsuariosSqlRepository usuariosRepository,
                                  IMapper mapper)
            : base(mapper)
        {
            _procuracoesPartesSqlRepository = procuracoesPartesSqlRepository;
            _usuariosRepository = usuariosRepository;
        }

        public override async Task Create(Outorgados outorgado)
        {
            if (outorgado.IdPessoa.HasValue)
            {
                var usuario = await _usuariosRepository.Buscar(u => u.IdPessoa == outorgado.IdPessoa.Value);
                if (usuario != null)
                    outorgado.Email = usuario.Email;

                var procuracoesPartes = ToDomain(outorgado, outorgado.IdPessoa.Value);
                await _procuracoesPartesSqlRepository.Incluir(procuracoesPartes);
            }
        }
    }
}
