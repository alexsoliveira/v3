// * Modules
import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { SolicitacaoRoutingModule } from './solicitacao.route';
import { NgxSpinnerModule } from 'ngx-spinner';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { SolicitacaoCadastroModule } from './nova-solicitacao/cadastro-procuracoes/solicitacao-cadastro.module';

// * Components
import { SolicitaAssinaturaComponent } from './solicitacao-assinatura/solicita-assinatura.component';
import { StatusSolicitacaoComponent } from './status-solicitacao/status-solicitacao.component';
import { PlayerPagamentoComponent } from './player-pagamento/player-pagamento.component';
import { TermoConcordanciaComponent } from './pagamento/carrinho/participante-carrinho/termo-concordancia/termo-concordancia.component';
import { CartaoCreditoComponent } from './player-pagamento/cartao-credito/cartao-credito.component';
import { BoletoBancarioComponent } from './player-pagamento/boleto-bancario/boleto-bancario.component';
import { TrackingComponent } from './status-solicitacao/tracking/tracking.component';
import { TrackingSolicitacaoComponent } from './status-solicitacao/tracking-solicitacao/tracking-solicitacao.component';
import { ParticipantePipe } from '../utils/participante.pipe';

import { NgBrazil } from 'ng-brazil';
import { TextMaskModule } from 'angular2-text-mask';

import { StringPipe } from '../utils/string.pipe';
import { FlexLayoutModule } from '@angular/flex-layout';
import { InfoComponent } from './status-solicitacao/info/info.component';
import { NgxMaskModule } from 'ngx-mask';
import { ParticipanteCarrinhoComponent } from './pagamento/carrinho/participante-carrinho/participante-carrinho.component';
import { CarrinhoComponent } from './pagamento/carrinho/carrinho.component';
import { ComposicaoDoPrecoComponent } from './pagamento/carrinho/participante-carrinho/composicao-do-preco/composicao-do-preco.component';

import { DetalhesComponent } from './status-solicitacao/detalhes/detalhes.component';
import { CepComponent } from '../shared/cep/cep.component';
import { WizardComponent } from './solicitacao-assinatura/wizard/wizard.component';


import { SolicitacaoGuard } from './nova-solicitacao/cadastro-procuracoes/services/solicitacao.guard';
import { ValidacaoDocumentoAssinadoComponent } from './solicitacao-assinatura/validacao-documento-assinado/validacao-documento-assinado.component';

@NgModule({
  declarations: [
    PlayerPagamentoComponent,
    StatusSolicitacaoComponent,
    TermoConcordanciaComponent,
    CartaoCreditoComponent,
    BoletoBancarioComponent,
    TrackingComponent,
    TrackingSolicitacaoComponent,
    ParticipantePipe,
    CarrinhoComponent,
    ParticipanteCarrinhoComponent,
    ComposicaoDoPrecoComponent,
    StringPipe,
    InfoComponent,
    DetalhesComponent,
    CepComponent,
    WizardComponent,
    ValidacaoDocumentoAssinadoComponent,
    SolicitaAssinaturaComponent
  ],
  imports: [
    CommonModule,
    SolicitacaoRoutingModule,
    SharedModule,
    NgxSpinnerModule,
    ReactiveFormsModule,
    FormsModule,
    NgxMaskModule.forChild(),
    TextMaskModule,
    NgBrazil,
    FlexLayoutModule,
    SolicitacaoCadastroModule
  ],
  providers: [
    SolicitacaoGuard,
    DatePipe
  ]
})
export class SolicitacaoModule { }
