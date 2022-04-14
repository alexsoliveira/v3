import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ToastOptions } from 'src/app/utils/toastr.config';
import { TermoUso } from '../models/termo-uso.model';
import { ContaService } from '../services/conta.service';

@Component({
  selector: 'app-termo-cookie',
  templateUrl: './termo-cookie.component.html',
  styleUrls: ['./termo-cookie.component.scss']
})
export class TermoCookieComponent implements OnInit {

  @Output() isTermosAceito = new EventEmitter();
  public termo: TermoUso = new TermoUso();

  constructor(
    private contaService: ContaService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.ObterTermoUso();
  }   

  ObterTermoUso(): void{
    this.contaService.ObterTermoUso('TermoUso')
    .subscribe((ret:TermoUso) => {
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

  termosUso(obj: any): any {
    this.isTermosAceito.emit({ termoAceito: obj.target.checked});
  } 

}
