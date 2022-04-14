// * Modules
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// * Components
import { LoginComponent } from './login/login.component';
import { RecuperarSenhaComponent } from './recuperar-senha/recuperar-senha.component';
import { CadastroComponent } from './cadastro/cadastro.component';
import { ConfirmarEmailComponent } from './confirmar-email/confirmar-email.component';
import { ResetarSenhaComponent } from './resetar-senha/resetar-senha.component';
import { EnviarEmailAtivacaoComponent } from './enviar-email-ativacao/enviar-email-ativacao.component';
import { UsuarioContaComponent } from './usuario/usuario-conta/usuario-conta.component';
import { UsuarioAlterarSenhaComponent } from './usuario/usuario-alterar-senha/usuario-alterar-senha.component';
import { AppGuardGuard } from '../app-guard.guard';

const contaRouterConfig: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'recuperar-senha', component: RecuperarSenhaComponent },
    { path: 'cadastro', component: CadastroComponent },
    { path: 'confirmar-email', component: ConfirmarEmailComponent },
    { path: 'resetar-senha', component: ResetarSenhaComponent },
    { path: 'reenviar-email', component: EnviarEmailAtivacaoComponent },
    { path: 'usuario-conta', component: UsuarioContaComponent, canActivate: [AppGuardGuard] },
    { path: 'usuario-alterar-senha', component: UsuarioAlterarSenhaComponent, canActivate: [AppGuardGuard] }
];

@NgModule({
    imports: [
        RouterModule.forChild(contaRouterConfig)
    ],
    exports: [RouterModule]
})
export class ContaRoutingModule {}
