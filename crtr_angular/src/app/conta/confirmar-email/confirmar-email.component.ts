import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { ContaService } from '../services/conta.service';
import { ToastOptions } from '../../utils/toastr.config';
import { UsuarioConfirmaConta } from '../models/ContaViewModel';

@Component({
  selector: 'app-confirmar-email',
  templateUrl: './confirmar-email.component.html',
  styleUrls: ['./confirmar-email.component.scss']
})
export class ConfirmarEmailComponent implements OnInit {

  constructor(
    private contaService: ContaService,
    private toastr: ToastrService,
    private router: Router,
    private spinner: NgxSpinnerService,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
  }

  ConfirmarEmail(){
    const dados = new UsuarioConfirmaConta();
    dados.userId = this.route.snapshot.queryParams["userId"];
    dados.code = this.route.snapshot.queryParams["code"];

    this.spinner.show();
    this.contaService.ConfirmarEmailAtivacao(dados)
    .then(res => {
       this.toastr.success("E-mail confirmado com sucesso! Em instantes você será redirecionado para a tela de login", "Tabelionet", ToastOptions)
      .onHidden.pipe().subscribe(() => this.router.navigateByUrl('/conta/login'));
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
