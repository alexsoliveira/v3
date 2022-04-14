import { SolicitacaoCadastroComponent } from './../solicitacao-cadastro.component';
//import { NovaSolicitacaoComponent } from './../nova-solicitacao/nova-solicitacao.component';
import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';

@Injectable()
export class SolicitacaoGuard implements CanDeactivate<SolicitacaoCadastroComponent> {

  canDeactivate(component: SolicitacaoCadastroComponent) {
    // console.log(component.mudancasNaoSalvas);
    

    if (component.mudancasNaoSalvas){
      return window.confirm('Tem certeza que deseja abandonar o preenchimento do formulário?');
    }
    return true;
  }

}
