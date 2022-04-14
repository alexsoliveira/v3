import { Component, OnInit, Input } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ToastOptions } from '../../../utils/toastr.config';
import { SolicitacaoService } from '../../services/solicitacao.service';
import { SolicitacaoPartesEstadosService } from '../../services/solicitacaoPartesEstados.service';
import { SolicitacaoPartesService } from '../../services/solicitacaoPartes.service';
import { StatusSolicitacao } from './../models/status-solicitacao.model';
import { DateUtils } from 'src/app/utils/date.utils';

@Component({
  selector: 'app-detalhes',
  templateUrl: './detalhes.component.html',
  styleUrls: ['./detalhes.component.scss']
})
export class DetalhesComponent implements OnInit {

  @Input() idSolicitacao: any;

  dataSource: MatTableDataSource<any>;
  columnsHeader = ['Participante', 'Documento', 'Tipo']; //, 'Operacao'
  columnsName = ['Participante', 'Documento', 'Tipo']; //, 'Operacao'
  expandedElement: StatusSolicitacao | null;

  dados: any;

  statusSolicitacao: StatusSolicitacao[] = [];

  constructor(
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private solicitacaoService: SolicitacaoService,
    private dateUtils: DateUtils,
    private solicitacaoPartesService: SolicitacaoPartesService,
    private solicitacaoPartesEstadosService: SolicitacaoPartesEstadosService,
  ) { }

  ngOnInit() {
    this.spinner.show();
    this.CarregarDados(this.idSolicitacao);
  }

  CarregarDados(idSolicitacao: any): void {
    this.solicitacaoService.StatusSolicitacaoPorParticipantes(idSolicitacao)
      .subscribe((statusSolicitacao) => {
        this.dados = statusSolicitacao;
        this.statusSolicitacao = statusSolicitacao;
        console.log(this.statusSolicitacao);
        this.dataSource = new MatTableDataSource(this.statusSolicitacao);
        console.log(this.dataSource);
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
        })
      .add(() => {
        this.spinner.hide();
      });
  }
}
