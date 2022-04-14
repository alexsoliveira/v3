import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ToastOptions } from 'src/app/utils/toastr.config';
import { CarrinhoService } from '../../services/carrinho.service';

import { TermoConcordancia } from '../../models/termoConcordancia.model'

@Component({
  selector: 'app-termo-concordancia',
  templateUrl: './termo-concordancia.component.html',
  styleUrls: ['./termo-concordancia.component.scss']
})
export class TermoConcordanciaComponent implements OnInit {

  public isTermosAceito = false;
  public termo: TermoConcordancia = new TermoConcordancia();

  constructor(
    private carrinhoService: CarrinhoService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.ObterTermoConcordancia();
  }

  ObterTermoConcordancia(): void{
    this.carrinhoService.ObterTermoConcordancia('TermoConcordancia')
    .subscribe((ret:TermoConcordancia) => {
      this.termo = ret;
      console.log(this.termo);
    },
    (err) => {
      console.log(err);
      try {
        try {
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
        this.toastr.error(`${err.status} - ${err.message} - ${err.error}`, 'Tabelionet', ToastOptions);
      }
    },
    () => { }
    ).add(() => {
      this.spinner.hide();
    });

  }

  public termosConcordancia(obj: any): any {
    this.isTermosAceito = obj.target.checked;
  }
}
