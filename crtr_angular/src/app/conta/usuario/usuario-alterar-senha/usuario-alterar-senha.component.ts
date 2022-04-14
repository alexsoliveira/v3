import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl, FormGroupName } from '@angular/forms';
import { ParentErrorStateMatcher, ErrorMessages, MatchValidator } from '../../../validators/custom.validator';
import { ContaService } from '../../services/conta.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Router, ActivatedRoute } from '@angular/router';

import { ToastOptions } from '../../../utils/toastr.config';
import { LocalStorageUtils } from 'src/app/utils/localstorage';
import { UsuarioAlterarSenha } from '../../models/ContaViewModel';

@Component({
  selector: 'app-usuario-alterar-senha',
  templateUrl: './usuario-alterar-senha.component.html',
  styleUrls: ['./usuario-alterar-senha.component.scss']
})
export class UsuarioAlterarSenhaComponent implements OnInit {

  formGroup: FormGroup;
  matching_passwords_group: FormGroup;
  parentErrorStateMatcher = new ParentErrorStateMatcher();
  errorMessages = ErrorMessages;

  localStorageUtils = new LocalStorageUtils();

  constructor(
    private fb: FormBuilder,
    private contaService: ContaService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  ngOnInit() {
    this.matching_passwords_group = new FormGroup({
      novaSenha: new FormControl('', Validators.compose([
        Validators.minLength(6),
        Validators.required,
        Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]+$')
      ])),
      confirma_senha: new FormControl('', Validators.required)
    }, (formGroupsenhas: FormGroup) => {
      return MatchValidator.areEqual(formGroupsenhas);
    });

    this.formGroup = this.fb.group({
      senha: new FormControl('', Validators.compose([
        Validators.minLength(6),
        Validators.required,
        Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]+$')
      ])),
      matching_passwords: this.matching_passwords_group
    });
  }


  AlterarSenha(): void {
    const dados = new UsuarioAlterarSenha();
    dados.senhaAtual = this.formGroup.get('senha').value;
    dados.novaSenha = this.formGroup.get('matching_passwords').get('novaSenha').value;
    dados.userId = this.localStorageUtils.lerUsuario().id;

    this.spinner.show();

    this.contaService.AlterarSenha(dados)
      .subscribe(res => {        
        this.spinner.hide(); 
        this.localStorageUtils.remover();
        this.toastr.success("Senha alterada com sucesso! Em instantes você será redirecionado para a tela de login.", "Tabelionet", ToastOptions)
        .onHidden.pipe().subscribe(() => this.router.navigateByUrl('/conta/login'));
      },
      (err) => {
        this.spinner.hide();        
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
            this.toastr.error(`${err.status} - ${err.message}`, 'Tabelionet', ToastOptions);
          }
      });      
  }

}
