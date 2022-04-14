import { ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { UsuarioConta } from '../models/ContaViewModel';
import { ContaService } from '../services/conta.service';
import { ToastOptions } from 'src/app/utils/toastr.config';

@Component({
  selector: 'app-enviar-email-ativacao',
  templateUrl: './enviar-email-ativacao.component.html',
  styleUrls: ['./enviar-email-ativacao.component.scss']
})
export class EnviarEmailAtivacaoComponent implements OnInit {

  email: string;

  constructor(
    private contaService: ContaService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.email = this.route.snapshot.queryParamMap.get('e');
  }

  enviar(): void {
    this.spinner.show();
    const dados = new UsuarioConta();
    dados.Email = this.email;

    this.spinner.show();
    this.contaService.EnviarEmailAtivacao(dados)
      .then(res => {
        this.toastr.success("E-mail de ativação de conta enviado com sucesso!", "Tabelionet", ToastOptions);
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
