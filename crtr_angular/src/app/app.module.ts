
// * Modules
import { NgModule, LOCALE_ID, DEFAULT_CURRENCY_CODE } from '@angular/core';
import { HashLocationStrategy, LocationStrategy, registerLocaleData } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FlexLayoutModule } from '@angular/flex-layout';
import localePt from '@angular/common/locales/pt';

import { NgxSpinnerModule } from 'ngx-spinner';
import { ToastrModule } from 'ngx-toastr';
import { MatProgressButtonsModule } from 'mat-progress-buttons';

import { AppRoutingModule } from './app-routing.module';
import { SolicitacaoModule } from './solicitacao/solicitacao.module';
import { ContaModule } from './conta/conta.module';
import { VitrineModule } from './vitrine/vitrine.module';
import { SharedModule } from './shared/shared.module';
import { PedidoModule } from './pedido/pedido.module';

// * Components
import { AppComponent } from './app.component';

// * Providers
import { authInterceptorProviders } from './helpers/auth.interceptor';

import { AppGuardGuard } from './app-guard.guard';
import { NgBrazil } from 'ng-brazil';
import { NgxMaskModule, IConfig } from 'ngx-mask';
import { NumeroPipe } from './utils/numero.pipe';

registerLocaleData(localePt);

export const options: Partial<IConfig> | (() => Partial<IConfig>) = null;

@NgModule({
  declarations: [
    AppComponent,
    NumeroPipe
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgxSpinnerModule,
    SharedModule,
    VitrineModule,
    ContaModule,
    SolicitacaoModule,
    PedidoModule,
    ToastrModule.forRoot(),
    MatProgressButtonsModule.forRoot(),
    NgxMaskModule.forRoot({
      dropSpecialCharacters: false
    }),
    NgBrazil,
    FlexLayoutModule
  ],
  providers: [
    AppGuardGuard,
    authInterceptorProviders,
    [{ provide: LOCALE_ID, useValue: "pt-BR" }],
    [{provide: DEFAULT_CURRENCY_CODE, useValue: 'BRL' }],
    [{ provide: LocationStrategy, useClass: HashLocationStrategy }]
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
