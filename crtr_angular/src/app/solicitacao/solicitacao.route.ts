import { SolicitacaoCadastroComponent } from './nova-solicitacao/cadastro-procuracoes/solicitacao-cadastro.component';
import { SolicitacaoGuard } from './nova-solicitacao/cadastro-procuracoes/services/solicitacao.guard';

import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { SolicitaAssinaturaComponent } from './solicitacao-assinatura/solicita-assinatura.component';
import { PlayerPagamentoComponent } from './player-pagamento/player-pagamento.component';
import { StatusSolicitacaoComponent } from './status-solicitacao/status-solicitacao.component';
import { CarrinhoComponent } from './pagamento/carrinho/carrinho.component';
import { AppGuardGuard } from '../app-guard.guard';
import { ValidacaoDocumentoAssinadoComponent } from './solicitacao-assinatura/validacao-documento-assinado/validacao-documento-assinado.component';

const solicitacaoRouterConfig: Routes = [
    // {
    //   path: 'nova-solicitacao/:idSolicitacao/:idProduto',
    //   component: SolicitacaoCadastroComponent,
    //   canDeactivate: [SolicitacaoGuard]
    // },
    {
      path: 'solicitacao-assinatura/:idSolicitacao/:idMatrimonio/:idPessoaSolicitante',
      component: SolicitaAssinaturaComponent,
      canActivate: [AppGuardGuard]
    },
    {
      path: 'validar-documento/:idMatrimonioDocumento',
      component: ValidacaoDocumentoAssinadoComponent,
      canActivate: [AppGuardGuard]
    },
    //{ path: 'carrinho/:idSolicitacao/:idProduto', component: CarrinhoComponent },
    {
      path: 'carrinho/:idSolicitacao',
      component: CarrinhoComponent,
      canActivate: [AppGuardGuard]
    },
    {
      path: 'player-pagamento/:idSolicitacao',
      component: PlayerPagamentoComponent,
      canActivate: [AppGuardGuard]
    },
    {
      path: 'status-solicitacao/:idSolicitacao',
      component: StatusSolicitacaoComponent,
      canActivate: [AppGuardGuard]
    },
    {
      path: 'nova-solicitacao/:produto/:modalidade',
      component: SolicitacaoCadastroComponent,
      canActivate: [AppGuardGuard],
      canDeactivate: [SolicitacaoGuard]
    },
    {
      path: 'nova-solicitacao/:idSolicitacao',
      component: SolicitacaoCadastroComponent,
      canActivate: [AppGuardGuard],
      canDeactivate: [SolicitacaoGuard]
    }
];

@NgModule({
    imports: [
        RouterModule.forChild(solicitacaoRouterConfig)
    ],
    exports: [RouterModule]
})
export class SolicitacaoRoutingModule {}
