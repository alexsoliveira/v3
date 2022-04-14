import { DateUtils } from './../../../utils/date.utils';
import { ContaService } from 'src/app/conta/services/conta.service';
import { MinhasSolicitacoesDataGrid } from './../../models/minhas-solicitacoes-datagrid.model';
import { Component, Inject, OnInit } from '@angular/core';
import { LocalStorageUtils } from 'src/app/utils/localstorage';
import { DOCUMENT } from '@angular/common';
import { ToastOptions } from 'src/app/utils/toastr.config';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { BoletoUtils } from './../../../utils/boleto.utils';


@Component({
  selector: 'app-datagrid-minhas-solicitacoes',
  templateUrl: './datagrid-minhas-solicitacoes.component.html',
  styleUrls: ['./datagrid-minhas-solicitacoes.component.scss']
})
export class DatagridMinhasSolicitacoesComponent implements OnInit {

  displayedColumns: string[] = ['idSolicitacao', 'participacao', 'produto', 'dataSolicitacao', 'estado', 'ultimaInteracao', 'acoes'];
  dataSource: MinhasSolicitacoesDataGrid[] = [];
  localStorageUtils = new LocalStorageUtils();

  constructor(
    private contaService: ContaService,
    private dateUtils: DateUtils,
    @Inject(DOCUMENT) private document: Document,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private boletoUtils: BoletoUtils
  ) { }

  ngOnInit(): void {
    this.CarregarGrid();
  }

  CarregarGrid(): void {
    let _u = this.localStorageUtils.lerUsuario();
    if (_u != null) {
      this.contaService.MinhasSolicitacoes(_u.idUsuario)
        .subscribe(grid => {
          if (grid && grid.length > 0) {
            this.dataSource = grid;
          }
        })
    }
  }

  goToStatusSolicitacao(): void {
    alert('deu certo!');
  }

  consultarBoleto(idSolicitacao: number): void {
    if (idSolicitacao != null) {
      console.log(idSolicitacao);

      this.spinner.show();
      this.contaService.ConsultarBoleto(idSolicitacao).subscribe(
        identifier => {
          this.spinner.hide();
          console.log(identifier);
                    
          if (identifier) {
            let linkBoleto = this.boletoUtils.getLinkBoleto(identifier);

            window.open(
              linkBoleto,
              "_blank",
              'noopener'
            );            
          } else {
            this.toastr.error(`Boleto não localizado`, 'Tabelionet', ToastOptions);    
          }                    

        }, (err) => {
          this.spinner.hide();
          this.toastr.error(`Ocorreu um erro ao tentar gerar o boleto!`, 'Tabelionet', ToastOptions);
          try {
            err.error.errors.Mensagens.forEach(erro => {
              this.toastr.error(`Boleto não localizado`, 'Tabelionet', ToastOptions);
            });
          }
          catch {
            Object.keys(err.error.errors).forEach((erro, i, a) => {
              err.error.errors[erro].forEach(msg => {
                this.toastr.error(`Boleto não localizado`, 'Tabelionet', ToastOptions);
              });
            });
          }
        }
      );
    }
  }

}
