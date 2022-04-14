import { element } from 'protractor';
import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { ToastrService } from 'ngx-toastr';
import { BehaviorSubject } from 'rxjs';
import { NgxSpinnerService } from 'ngx-spinner';

import { ToastOptions } from 'src/app/utils/toastr.config';

import { SolicitacaoService } from '../../../../services/solicitacao.service';
import { CarrinhoService } from './../../services/carrinho.service';

import { ComposicaoPreco } from '../../models/composicaoPreco.model';
import { ComposicaoPrecoDataGrid } from '../../models/composicaoPrecoDataGrid.model';

@Component({
  selector: 'app-composicao-do-preco',
  templateUrl: './composicao-do-preco.component.html',
  styleUrls: ['./composicao-do-preco.component.scss']
})
export class ComposicaoDoPrecoComponent implements OnInit {

  @Input() dataSource: ComposicaoPrecoDataGrid[] = [];
  composicaoPreco: ComposicaoPreco[] = [];
  idSolicitacao: number;
  dadosComposicaoPrecoRecebido: boolean = false;
  dadosComposicaoPrecoEmolumentoRecebido: boolean = false;

  @Input() bsCompPreco: BehaviorSubject<any>;
  // @Input() SolAtoCart: ComposicaoPreco[];
  public valorTotal = '0';
  //displayedColumns: string[] = ['idPessoa', 'opcao', 'titulo', 'descricao', 'custo'];
  //displayedColumns: string[] = ['servico','observacao','destino','custo'];
  displayedColumns: string[] = ['servico','destino','custo'];
  columnsToDisplay: string[] = this.displayedColumns.slice();
  //datasource: MatTableDataSource<any>;
  table: MatTable<ComposicaoPrecoDataGrid>;

  constructor(
    //private solicitacaoService: SolicitacaoService,
    private carrinhoService: CarrinhoService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private activatedRoute: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.idSolicitacao = this.activatedRoute.snapshot.params.idSolicitacao;

    this.ObterComposicaoPrecos(this.idSolicitacao);
    this.ObterTaxaEmolumento(this.idSolicitacao);
    
    let interval = setInterval(() => {
      if(this.dadosComposicaoPrecoEmolumentoRecebido
        && this.dadosComposicaoPrecoRecebido){
          this.CalculaItens();
          clearInterval(interval);
        }
    }, 500);
  }

  ObterTaxaEmolumento(idSolicitacao: number): void {
    this.carrinhoService.ObterTaxaEmolumentos(idSolicitacao)
      .subscribe((taxas) => {
        this.composicaoPreco.push({
          disponivel: true,
          servico: 'Taxa de Emolumento',
          observacao: '',
          destino: 'Cartório',
          custo: parseFloat(taxas[0].valorTaxa).toFixed(2)
        });
        this.dadosComposicaoPrecoEmolumentoRecebido = true;
      })
  }

  ObterComposicaoPrecos(idSolicitacao: number): void{
    this.carrinhoService.ObterComposicaoPrecos(idSolicitacao)
    .subscribe((ret: ComposicaoPreco[]) => {
      ret.forEach((compPreco) => {
        this.composicaoPreco.push({
          disponivel: compPreco.disponivel,
          servico: compPreco.servico,
          observacao: compPreco.observacao,
          destino: compPreco.destino,
          custo: compPreco.custo
        });
      })
      this.dadosComposicaoPrecoRecebido = true;
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

  CalculaItens(): void {
    this.dataSource = this.composicaoPreco;
    this.valorTotal = '0';
    this.composicaoPreco.forEach((element) => {
      this.valorTotal = parseFloat((parseFloat(this.valorTotal) + parseFloat(element.custo)).toString()).toFixed(2);
    });
  }
}
