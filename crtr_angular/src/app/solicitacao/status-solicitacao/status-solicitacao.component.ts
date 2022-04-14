import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ToastOptions } from 'src/app/utils/toastr.config';
import { DirecionamentoService } from '../services/direcionamento.service';
import { SolicitacaoService } from '../services/solicitacao.service';
import { StatusSolicitacaoHeader } from './models/status-solicitacao-header.model';

@Component({
  selector: 'app-status-solicitacao',
  templateUrl: './status-solicitacao.component.html',
  styleUrls: ['./status-solicitacao.component.scss']
})
export class StatusSolicitacaoComponent implements OnInit {

  idSolicitacao: any;
  idProduto: any;
  obj: any;
  statusSolicitacaoHeader: StatusSolicitacaoHeader;

  constructor(
    private activatedRoute: ActivatedRoute,
    private solicitacaoService: SolicitacaoService,
    private toastr: ToastrService,
    private router: Router,
    private direcionamento: DirecionamentoService
  ) { }

  ngOnInit(): void {

    this.idSolicitacao = this.activatedRoute.snapshot.params.idSolicitacao;
    this.idProduto = this.activatedRoute.snapshot.params.idProduto;

    this.direcionamento.direcionarStatusSolicitacao('status-solicitacao', this.idSolicitacao);

    this.obj = JSON.parse(localStorage.getItem('_c'));

    this.buscarDadosSolicitacaoHeader();
  }

  buscarDadosSolicitacaoHeader(): void {
    this.solicitacaoService.BuscarDadosSolicitacaoHeader(this.idSolicitacao)
      .subscribe((statusSolicitacaoHeader) => {
        if (statusSolicitacaoHeader) {
          this.statusSolicitacaoHeader = statusSolicitacaoHeader;
        } else {
          this.toastr.error('Não foi possível buscar os dados da solicitação!', 'Tabelionet', ToastOptions);
        }
      },
        (err) => {
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
        })
  }

  printTelaStatusSolicitacao(componente) {  
    let w = window.open('', '_blank', 'width=1200,height=900,scrollbars=no,menubar=no,toolbar=no,location=no,status=no,titlebar=no');
    w.location.href = '/#/status-solicitacao/' + this.idSolicitacao;
    let asdf = setTimeout(() => {
      w.print();
      w.close();
      clearTimeout(asdf)
    }
      , 1500);

  }

}
