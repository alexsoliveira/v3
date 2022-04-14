using System.Collections.Generic;
using TGS.Cartorio.Application.DTO;
using TGS.Cartorio.Application.DTO.Relatorios;

namespace TGS.Cartorio.Application.Relatorios.Interfaces
{
    public interface IPdfSolicitacaoReport
    {
        void SetDadosSolicitacao(SolicitacaoProntaParaEnvioDto solicitacaoPronta);
        ValidadorEnvioEmailSolicitacaoCartorioDto GerarReport(string razaoSocialCartorio, string emailCartorio);
    }
}
