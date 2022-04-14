import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { ContaService } from '../services/conta.service';
import { UsuarioConta } from '../models/ContaViewModel';
import { ToastOptions } from 'src/app/utils/toastr.config';

@Component({
  selector: 'app-recuperar-senha',
  templateUrl: './recuperar-senha.component.html',
  styleUrls: ['./recuperar-senha.component.scss']
})
export class RecuperarSenhaComponent implements OnInit {

  email: string;

  constructor(
    private snackBar: MatSnackBar,
    private router: Router,
    private contaService: ContaService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
  }

  enviar(): void {

    console.log(this.email);

    const dados = new UsuarioConta();
    dados.Email = this.email;

    this.spinner.show();
    this.contaService.EnviarEmailResetSenha(dados)
    .then(res => {
      this.toastr.success("E-mail de reset de senha enviado com sucesso!", "Tabelionet", ToastOptions);
    })
    .finally(() => {
      this.spinner.hide();
    })
    .catch(err => {
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
}
