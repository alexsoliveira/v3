import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Router, ActivatedRoute } from '@angular/router';

import { ContaService } from '../services/conta.service';

import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

import { UsuarioResetSenha } from '../models/ContaViewModel';
import { ToastOptions } from '../../utils/toastr.config';

import { ErrorMessages, MatchValidator, ParentErrorStateMatcher } from 'src/app/validators/custom.validator';

@Component({
  selector: 'app-resetar-senha',
  templateUrl: './resetar-senha.component.html',
  styleUrls: ['./resetar-senha.component.scss']
})
export class ResetarSenhaComponent implements OnInit {

  resetSenhaForm: FormGroup;
  matching_passwords_group: FormGroup;

  parentErrorStateMatcher = new ParentErrorStateMatcher();
  errorMessages = ErrorMessages;

  constructor(
    private fb: FormBuilder,
    private contaService: ContaService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.createForm();
  }

  createForm(): void {
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

    this.resetSenhaForm = this.fb.group({
      matching_passwords: this.matching_passwords_group
    });
  }

  ResetarSenha(): void {
    const dados = new UsuarioResetSenha();
    dados.senha = this.resetSenhaForm.get('matching_passwords').get('senha').value;
    dados.userId = this.route.snapshot.queryParams["userId"];
    dados.code = this.route.snapshot.queryParams["code"];

    this.spinner.show();

    this.contaService.ResetarSenha(dados)
      .then(res => {
        this.toastr.success("Senha resetada com sucesso! Em instantes você será redirecionado para a tela de login.", "Tabelionet", ToastOptions)
        .onHidden.pipe().subscribe(() => this.router.navigateByUrl('/conta/login'));
      })
      .finally(() => {
        this.spinner.hide();
      })
      .catch(error => {
        this.toastr.error(`${error.status} - ${error.message}`, 'Tabelionet', ToastOptions);
      });
  }
}
