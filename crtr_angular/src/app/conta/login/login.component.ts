import { ToastOptions } from './../../utils/toastr.config';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ErrorMessages } from './../../validators/custom.validator';

import { ContaService } from 'src/app/conta/services/conta.service';

import { UsuarioLogin, UsuarioRespostaLogin } from '../models/ContaViewModel';

import { LocalStorageUtils } from './../../utils/localstorage';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  localStorageUtils = new LocalStorageUtils();

  loginForm: FormGroup;

  msgErros = ErrorMessages;

  constructor(
    private contaService: ContaService,
    private router: Router,
    private route: ActivatedRoute,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      email: ['', [
        Validators.required,
        Validators.email
      ]],
      password: ['', [
        Validators.required,
        Validators.minLength(6),
        Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]+$')
      ]],
    });
  }

  Login(): void {

    const dados = new UsuarioLogin();
    dados.email = this.loginForm.get('email').value;
    dados.senha = this.loginForm.get('password').value;

    this.spinner.show();
    this.contaService.Login(dados)
      .then((res: UsuarioRespostaLogin) => {

        if(res.contaAtivada){
          
          this.localStorageUtils.salvarToken(res.accessToken);
          this.localStorageUtils.salvarUsuario(res.usuarioToken);
          
          let paramNavigate = this.route.snapshot.queryParamMap.get('navigate');
          if (paramNavigate) {
            this.router.navigateByUrl(paramNavigate);
          }
          else
          {
            this.router.navigateByUrl('/home');
          }
        }
        else
        {
          this.toastr.error("Conta de usuário desativada.", 'Tabelionet', ToastOptions);
          this.router.navigate(['/conta/reenviar-email'], { queryParams: { e: this.loginForm.get('email').value } });
        }
      })
      .finally(() => {
        this.spinner.hide();
      })
      .catch((err) => {
        try {
          try {
            err.error.errors.Mensagens.forEach(erro => {
              this.toastr.error(`${erro}`, 'Tabelionet', ToastOptions);
            });
          }
          catch {
            Object.keys(err.error.errors).forEach((erro,i,a) => {
              err.error.errors[erro].forEach(msg => {
                this.toastr.error(`${msg}`, 'Tabelionet', ToastOptions);
              });
            });
          }
        }
        catch {
          this.toastr.error(`${err.status} - ${err.message} - ${err.error}`, 'Tabelionet', ToastOptions);
        }
      });
  }
}
