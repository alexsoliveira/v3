export class SolicitacoesEstadosPc {
    idSolicitacoesEstadosPc : number;
    descricao: string;
    status: string;

    constructor(idSolicitacaoEstadoPc: number, descricao: string, status: string){
      this.idSolicitacoesEstadosPc = idSolicitacaoEstadoPc;
      this.descricao = descricao;
      this.status = status;
    }

    static getListTracking(): SolicitacoesEstadosPc[] {
      let list = [];

      list.push(new SolicitacoesEstadosPc(0, 'Solicitação Cadastrada', ''));
      list.push(new SolicitacoesEstadosPc(6, 'Assinatura Digital do Solicitante',''));
      list.push(new SolicitacoesEstadosPc(7, 'Aceite do Carrinho', ''));
      list.push(new SolicitacoesEstadosPc(11, 'Pagamento Confirmado', 'Solicitação Pronta Para Envio ao Cartório'));
      list.push(new SolicitacoesEstadosPc(9, 'Solicitaçao Enviada Ao Cartório', 'Solicitaçao Enviada Ao Cartório'));
      // list.push(new SolicitacoesEstadosPc(10, 'Envio Confirmado', 'Envio Ao Cartório Confirmado'));

      return list;
    }
  }


  // como estava antes
  // list.push(new SolicitacoesEstadosPc(0, 'Solicitação Cadastrada', 'Cadastrada'));
  //     list.push(new SolicitacoesEstadosPc(12, 'Aguardando Assinatura Digital do Solicitante', 'Aguardando Assinatura Digital do Solicitante'));
  //     list.push(new SolicitacoesEstadosPc(6, 'Aguardando Aceite do Carrinho', 'Aguardando Aceite do Carrinho'));
  //     list.push(new SolicitacoesEstadosPc(7, 'Aguardando Pagamento', 'Aguardando Efetuar Pagamento'));
  //     list.push(new SolicitacoesEstadosPc(11, 'Pagamento Confirmado', 'Solicitação Pronta Para Envio ao Cartório'));
  //     list.push(new SolicitacoesEstadosPc(9, 'Solicitaçao Enviada Ao Cartório', 'Solicitaçao Enviada Ao Cartório'));
  //     list.push(new SolicitacoesEstadosPc(10, 'Envio Confirmado', 'Envio Ao Cartório Confirmado'));