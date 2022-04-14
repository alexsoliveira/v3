import { Router } from '@angular/router';
import { Component, OnInit, AfterViewInit, Input } from '@angular/core';
import { FormControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { ContaService } from '../services/conta.service';
import { GeneroService } from '../../shared/services/GeneroService.service';
import { Genero } from '../../shared/models/genero.model';

import { ParentErrorStateMatcher, MatchValidator, ErrorMessages } from './../../validators/custom.validator';

import { ToastOptions } from './../../utils/toastr.config';

import { UsuarioConta, UsuarioRegistro } from '../models/ContaViewModel';
import { utilsBr } from 'js-brasil';
import { NgBrazilValidators } from 'ng-brazil';
import { HtmlSelect } from 'src/app/shared/services/HtmlSelect.service';

@Component({
  selector: 'app-cadastro',
  templateUrl: './cadastro.component.html',
  styleUrls: ['./cadastro.component.scss']
})
export class CadastroComponent implements OnInit, AfterViewInit {

  cadastroForm: FormGroup;
  matching_passwords_group: FormGroup;
  matching_emails_group: FormGroup;
  @Input() TermoAceito: boolean;

  parentErrorStateMatcher = new ParentErrorStateMatcher();
  errorMessages = ErrorMessages;
  selectTipoDocumentos: any[] = [];
  selectGeneros: Genero[] = [];
  exibirRg: boolean = true;
  habilitarBotaoCriarConta: boolean = true;

  emailCadastrado: string = "";

  MASKS = utilsBr.MASKS;

  constructor(
    private fb: FormBuilder,
    private contaService: ContaService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private router: Router,
    private htmlSelect: HtmlSelect,
    private generoService: GeneroService
  ) { }

  ngOnInit(): void {
    this.createForm();
    this.selectTipoDocumentos = this.htmlSelect.getTipoDocumento();
    this.buscarSetarGeneros();
  }

  ngAfterViewInit(): void { }

  createForm(): void {

    this.matching_emails_group = new FormGroup({
      email: new FormControl('', Validators.compose([
        Validators.required,
        Validators.email
      ])),
      confirma_email: new FormControl('', Validators.required)
    }, (formGroupemail: FormGroup) => {
      return MatchValidator.areEqual(formGroupemail);
    });

    this.matching_passwords_group = new FormGroup({
      senha: new FormControl('', Validators.compose([
        Validators.minLength(6),
        Validators.required,
        Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]+$')
      ])),
      confirma_senha: new FormControl('', Validators.required)
    }, (formGroupsenhas: FormGroup) => {
      return MatchValidator.areEqual(formGroupsenhas);
    });

    this.cadastroForm = this.fb.group({
      nome: ['', Validators.required],
      nomeSocial: ['', Validators.required],
      idTipoDocumento: ['', Validators.required],
      documento: ['', Validators.compose([Validators.required, NgBrazilValidators.cpf])],
      rg: ['', Validators.required],
      idGenero: ['', Validators.required],
      matching_passwords: this.matching_passwords_group,
      matching_emails: this.matching_emails_group,
    });
  }

  Cadastrar(): void {
    this.habilitarBotaoCriarConta = false;
    const dados = new UsuarioRegistro();
    dados.nome = this.cadastroForm.get('nome').value;
    dados.nomeSocial = this.cadastroForm.get('nomeSocial').value;
    dados.documento = this.cadastroForm.get('documento').value;
    dados.idTipoDocumento = this.cadastroForm.get('idTipoDocumento').value;
    dados.rg = this.cadastroForm.get('rg').value;
    dados.idGenero = this.cadastroForm.get('idGenero').value;
    dados.email = this.cadastroForm.get('matching_emails').get('email').value;
    this.emailCadastrado = this.cadastroForm.get('matching_emails').get('email').value;
    dados.senha = this.cadastroForm.get('matching_passwords').get('senha').value;
    dados.senhaConfirmacao = this.cadastroForm.get('matching_passwords').get('confirma_senha').value;
    dados.termoAceito = this.TermoAceito;

    this.spinner.show();

    this.contaService.Cadastrar(dados)
      .then((res: boolean) => {

        console.log(res);

        if (res) {
          this.EnviarEmail(this.emailCadastrado);
          this.toastr.success('Cadastro efetuado com sucesso!. Um e-mail será enviado para ativar sua conta.', 'Tabelionet', ToastOptions);
        }
      })
      .finally(() => {
        console.log('fim');
        this.spinner.hide();
      })
      .catch(err => {
        try {
          try {
            this.habilitarBotaoCriarConta = true;
            err.error.errors.Mensagens.forEach(erro => {
              this.toastr.error(`${erro}`, 'Tabelionet', ToastOptions);
            });
          }
          catch {
            Object.keys(err.error.errors).forEach((erro, i, a) => {
              err.error.errors[erro].forEach(msg => {
                this.toastr.error(`${msg}`, 'Tabelionet', ToastOptions);
              });
            });
          }
        }
        catch {
          this.habilitarBotaoCriarConta = true;
          this.toastr.error(`${err.status} - ${err.message}`, 'Tabelionet', ToastOptions);
        }
      });
  }

  EnviarEmail(email: string): void {
    const objEmail = new UsuarioConta();
    objEmail.Email = this.emailCadastrado;

    this.contaService.EnviarEmailAtivacao(objEmail)
      .then((res: boolean) => {
        if (res) {
          this.toastr.success("E-mail enviado com sucesso!", 'Tabelionet', ToastOptions);
          this.router.navigate(['/login']);
        }
      })
      .finally(() => {
        console.log('fim');
      })
      .catch(err => {
        this.router.navigate(['/conta/reenviar-email'], { queryParams: { e: email } });
        try {
          err.error.errors.Mensagens.forEach(erro => {
            this.toastr.error(`${erro}`, 'Tabelionet', ToastOptions);
          });
        }
        catch {
          this.toastr.error(`${err.status} - ${err.message}`, 'Tabelionet', ToastOptions);
        }
      });
  }

  termosCookies(evento) {
    this.TermoAceito = evento.termoAceito;
  }

  validarBotao() {
    return (!this.cadastroForm.valid || !this.TermoAceito || !this.habilitarBotaoCriarConta);
  }


  validarTipoDocumento(option: any) {
    if (this.selectTipoDocumentos.find(p => p.valor == option.value).texto !== 'CPF') {
      this.cadastroForm.get('documento').setValidators(Validators.compose([Validators.required, NgBrazilValidators.cpf]));
      this.cadastroForm.get('rg').setValue('');
      this.cadastroForm.get('rg').clearValidators();
      this.exibirRg = false;
    } else {
      this.exibirRg = true;
      this.cadastroForm.get('rg').setValidators([Validators.required]);
      this.cadastroForm.get('documento').setValidators(Validators.compose([Validators.required, NgBrazilValidators.cpf]));
    }
  }

  buscarSetarGeneros(){
    this.generoService.obterGeneros()
      .subscribe((generos)=>{
        if (generos && generos.length > 0) {
          this.selectGeneros = generos;
        }
      })
  }
}
