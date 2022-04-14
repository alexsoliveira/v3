import { Component, OnInit, ViewChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { ComposicaoDoPrecoComponent } from '../../pagamento/carrinho/participante-carrinho/composicao-do-preco/composicao-do-preco.component';
import { CarrinhoService } from './services/carrinho.service';
import { SolicitacaoService } from '../../services/solicitacao.service';

import { ToastOptions } from '../../../utils/toastr.config';

import { Produtos } from '../../../vitrine/models/produtos.model';
import { CarrinhoCompras } from '../models/CarrinhoCompras.models';
import { ComposicaoPreco } from '../models/ComposicaoPreco.model';
import { Solicitante } from '../models/Solicitante.model';
import { Produto } from './models/produto.model';
import { DirecionamentoService } from '../../services/direcionamento.service';
//import { Precificacao } from './models/precificacao.model';

@Component({
  selector: 'app-carrinho',
  templateUrl: './carrinho.component.html',
  styleUrls: ['./carrinho.component.scss']
})
export class CarrinhoComponent implements OnInit {

  tituloTela: string = "CARRINHO DE COMPRAS";
  //bsCompPreco = new BehaviorSubject<any>(undefined);

  habGravar = true;
  produto: Produto = new Produto();
  //precificacao: Precificacao = new Precificacao();
  dados = new CarrinhoCompras();
  idSolicitacao: number = undefined;
  idProduto: number;
  taxaEmolumento: string;
  solicitante = new Solicitante();

  dadosCompPreco = new Array();
  // setTimeoutRedirecionarStatusSolicitacao = setInterval(() => this.redirecionarStatusSolicitacao(), 2000);
  // podeRedirecionarStatusSolicitacao: boolean = false;

  @ViewChild(ComposicaoDoPrecoComponent) composicaoPreco: ComposicaoDoPrecoComponent;

  constructor(
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private solicitacaoService: SolicitacaoService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private carrinhoService: CarrinhoService,
    private direcionamento: DirecionamentoService
  ) { }

  async ngOnInit() {
    this.idSolicitacao = this.activatedRoute.snapshot.params.idSolicitacao;
    await this.direcionamento.direcionarStatusSolicitacao('carrinho', this.idSolicitacao);
    this.BuscarSolicitante(this.idSolicitacao);
    this.ObterProduto(this.idSolicitacao);
  }

  BuscarSolicitante(id: number): void {
    this.carrinhoService.BuscarSolicitante(id)
      .subscribe((ret: Solicitante) => {
        this.solicitante = ret;
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

  ObterProduto(idSolicitacao: number): void {
    this.carrinhoService.ObterProduto(idSolicitacao)
      .subscribe((ret: Produto) => {
        this.produto = ret;

        console.log(this.produto);
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

  salvarDados(dados: any): void {

    // TODO: Pegar o objeto termo concordancia
    this.carrinhoService.AceiteTermoConcordancia(this.idSolicitacao, true)
      .subscribe(() => {
        console.log('AceiteTermoConcordancia');
        // setTimeout(() => this.podeRedirecionarStatusSolicitacao = true, 1000);

        const obj = {
          solicitacao: this.idSolicitacao,
          produto: this.produto.titulo,
          usuario: this.solicitante.nomeUsuario,
          valorTotal: this.composicaoPreco.valorTotal
        };
        localStorage.removeItem('_c');
        localStorage.setItem('_c', JSON.stringify(obj));
    
        this.toastr.success('Os termos foram aceitos com sucesso!', 'Tabelionet', ToastOptions)
          .onHidden.pipe().subscribe(() => {
            this.router.navigate([`/player-pagamento/${this.idSolicitacao}`]);
          });
        
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
}
