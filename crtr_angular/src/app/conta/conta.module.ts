
// * Modules
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ContaRoutingModule } from './conta.route';
import { FormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';
import { NgxSpinnerModule } from 'ngx-spinner';

// * Components
import { LoginComponent } from './login/login.component';
import { CadastroComponent } from './cadastro/cadastro.component';
import { RecuperarSenhaComponent } from './recuperar-senha/recuperar-senha.component';
import { ConfirmarEmailComponent } from './confirmar-email/confirmar-email.component';
import { ResetarSenhaComponent } from './resetar-senha/resetar-senha.component';
import { EnviarEmailAtivacaoComponent } from './enviar-email-ativacao/enviar-email-ativacao.component';
import { UsuarioContaComponent } from './usuario/usuario-conta/usuario-conta.component';
import { TextMaskModule } from 'angular2-text-mask';
import { FlexLayoutModule } from '@angular/flex-layout';
import { UsuarioAlterarSenhaComponent } from './usuario/usuario-alterar-senha/usuario-alterar-senha.component';
import { TermoCookieComponent } from './termo-cookie/termo-cookie.component';

// * Utils
import { CpfPipe } from './../utils/cpf.pipe';
import { DatagridMinhasSolicitacoesComponent } from './usuario/datagrid-minhas-solicitacoes/datagrid-minhas-solicitacoes.component';
import { NgxMaskModule } from 'ngx-mask';

@NgModule({
  declarations: [
    LoginComponent,
    CadastroComponent,
    RecuperarSenhaComponent,
    ConfirmarEmailComponent,
    ResetarSenhaComponent,
    EnviarEmailAtivacaoComponent,
    UsuarioContaComponent,
    UsuarioAlterarSenhaComponent,
    TermoCookieComponent,
    CpfPipe,
    DatagridMinhasSolicitacoesComponent
  ],
  imports: [
    CommonModule,
    ContaRoutingModule,
    NgxSpinnerModule,
    NgxMaskModule.forChild(),
    FormsModule,
    ReactiveFormsModule,
    FlexLayoutModule,
    SharedModule,
    TextMaskModule
  ]
})
export class ContaModule { }
