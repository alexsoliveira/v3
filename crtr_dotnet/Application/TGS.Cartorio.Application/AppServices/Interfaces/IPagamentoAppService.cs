using Hangfire.Server;
using System.Collections.Generic;
using System.Threading.Tasks;
using TGS.Cartorio.Application.DTO;
using TGS.Cartorio.Application.DTO.Pagamento;
using TGS.Cartorio.Infrastructure.Utility.ApiClient;

namespace TGS.Cartorio.Application.AppServices.Interfaces
{
    public interface IPagamentoAppService
    {
        Task<SimuladorParcelamentoDto> SimularParcelamento(decimal valorTotal, string token);
        Task<Retorno<RespostaPagamentoCartaoCreditoDto>> PagarComCartaoCredito(CartaoCreditoDto cartaoCredito, string token);
        Task<RespostaConsultaTransacaoDto> ConsultarTransacaoCartaoCreditoPorIdentifier(string identifier, string token);
        Task<RespostaConsultaTransacaoDto> ConsultarTransacaoCartaoCreditoPorSolicitacao(long idSolicitacao, string token);
        Task<RespostaConsultaBoleto> ConsultarPagamentoBoleto(PerformContext consoleHangFire, long idSolicitacao);
        Task<IEnumerable<SolicitacoesDto>> TodasSolicitacoesAguardandoPagamentoBoleto();
    }
}
