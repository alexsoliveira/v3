using System;
using TGS.Cartorio.Domain.Entities;

namespace TGS.Cartorio.Application.Enumerables
{
    public static class GerenciadorEstadosSolicitacao
    {
        public static int ProximoEstadoSolicitacao(int idSolicitacaoEstado, TelasSolicitacao telaSolicitacao = TelasSolicitacao.Outros)
        {
            try
            {
                int proximoEstado = idSolicitacaoEstado;
                EstadosSolicitacao estadoAtual = (EstadosSolicitacao)idSolicitacaoEstado;
                switch (estadoAtual)
                {
                    case EstadosSolicitacao.Cadastrada:
                        if (telaSolicitacao == TelasSolicitacao.NovaSolicitacao)
                            proximoEstado = (int)EstadosSolicitacao.AguardandoAssinaturaDigitalSolicitante;
                        break;

                    case EstadosSolicitacao.AguardandoAssinaturaDigitalSolicitante:
                        if (telaSolicitacao == TelasSolicitacao.AssinaturaDigitalSolicitante)
                            proximoEstado = (int)EstadosSolicitacao.AguardandoAceiteCarrinho;
                        break;

                    case EstadosSolicitacao.AguardandoAceiteCarrinho:
                        if (telaSolicitacao == TelasSolicitacao.Carrinho)
                            proximoEstado = (int)EstadosSolicitacao.AguardandoPagamento;
                        break;

                    case EstadosSolicitacao.AguardandoPagamento:
                        if (telaSolicitacao == TelasSolicitacao.Pagamento 
                            || telaSolicitacao == TelasSolicitacao.Job)
                            proximoEstado = (int)EstadosSolicitacao.SolicitacaoProntaParaEnvioCartorio;
                        break;


                    //OS ESTADOS ABAIXO DEPENDEM APENAS DO SERVIÇO DO WINDOWS PARA ATUALIZAR
                    case EstadosSolicitacao.SolicitacaoProntaParaEnvioCartorio:
                        if (telaSolicitacao == TelasSolicitacao.Job)
                            proximoEstado = (int)EstadosSolicitacao.SolicitacaoEnviadaAoCartorio;
                        break;

                    case EstadosSolicitacao.SolicitacaoEnviadaAoCartorio:
                        if (telaSolicitacao == TelasSolicitacao.Job)
                            proximoEstado = (int)EstadosSolicitacao.EnvioAoCartorioConfirmado;
                        break;

                    default:
                        break;
                }

                return proximoEstado;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
