import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { SolicitacaoService } from './solicitacao.service';

@Injectable({
  providedIn: 'root'
})
export class DirecionamentoService {

  constructor(private solicitacaoService: SolicitacaoService,
    private router: Router) { }

  async direcionarStatusSolicitacao(telaAtual: string, idSolicitacao: number) {

    var statusSolicitacao = await this.solicitacaoService.consultarStatusSolicitacao(idSolicitacao);

    let carrinho = statusSolicitacao.estadoAtual === 'Aguardando Aceite do Carrinho' 
                     && telaAtual === 'carrinho';
    let assinatura = statusSolicitacao.estadoAtual === 'Aguardando Assinatura Digital do Solicitante' 
                     && telaAtual === 'solicitacao-assinatura'; 
    let pagamento = statusSolicitacao.estadoAtual === 'Aguardando Efetuar Pagamento' 
                     && telaAtual === 'player-pagamento'; 
    let novaSolicitacao = statusSolicitacao.estadoAtual === 'Aguardando Assinatura Digital do Solicitante' 
                     && telaAtual === 'nova-solicitacao';
    let verificaStatusSolicitacao = statusSolicitacao.estadoAtual === 'Aguardando Efetuar Pagamento Boleto' 
    && telaAtual === 'status-solicitacao';
    
    if (carrinho || assinatura || pagamento || novaSolicitacao || verificaStatusSolicitacao) {
      return;
    }
    else if (statusSolicitacao.estadoAtual === 'Aguardando Efetuar Pagamento Boleto'){
      this.router.navigate([`status-solicitacao/${idSolicitacao}`]);
    }
    else if (statusSolicitacao.estadoAtual === 'Aguardando Assinatura Digital do Solicitante'
     && telaAtual !== 'solicitacao-assinatura') {
       let dadosOutorgante = await this.solicitacaoService.buscarDadosProcuracaoSolicitante(idSolicitacao);
       
       if (dadosOutorgante) {
        this.router.navigate([`solicitacao-assinatura/${idSolicitacao}/${dadosOutorgante.idMatrimonio}/${dadosOutorgante.idPessoa}`]);

       } else {
        this.router.navigate([`home`]);
       }

    } else if (statusSolicitacao.estadoAtual === 'Aguardando Aceite do Carrinho'
    && telaAtual !== 'carrinho') {
      this.router.navigate([`carrinho/${idSolicitacao}`]);
      
    } else if (statusSolicitacao.estadoAtual === 'Aguardando Efetuar Pagamento'
    && telaAtual !== 'player-pagamento') {
      this.router.navigate([`player-pagamento/${idSolicitacao}`]);

    } else {
      this.router.navigate([`status-solicitacao/${idSolicitacao}`]);
    }
  }

}
