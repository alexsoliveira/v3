namespace TGS.Cartorio.Application.Enumerables
{
    public enum EstadosSolicitacao
    {
        Cadastrada = 0,
        AguardandoAceiteCarrinho = 6,
        AguardandoPagamento = 7,
        AguardandoAtualizacaoDados = 8,
        SolicitacaoEnviadaAoCartorio = 9,
        EnvioAoCartorioConfirmado = 10,
        SolicitacaoProntaParaEnvioCartorio = 11,
        AguardandoAssinaturaDigitalSolicitante = 12
    }


    public enum TelasSolicitacao
    {
        AssinaturaDigitalSolicitante,
        NovaSolicitacao,
        Carrinho,
        Pagamento,
        Outros,
        Job
    }
}