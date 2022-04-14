import { EnderecoNovoComponent } from './../../shared/endereco-novo/endereco-novo.component';
import { AfterContentInit, Component, OnInit, ViewChild, AfterViewInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatRadioButton } from '@angular/material/radio';
import { Endereco } from '../../shared/models/Endereco.model';
import { MatDialog } from '@angular/material/dialog';
import { ModalComponent } from '../../shared/modal/modal.component';
import { EnderecoService } from '../../shared/services/Endereco.service';
import { LocalStorageUtils } from 'src/app/utils/localstorage';
import { BehaviorSubject } from 'rxjs';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ToastOptions } from '../../utils/toastr.config';
import { DirecionamentoService } from '../services/direcionamento.service';
import { PagamentoService } from '../services/pagamento.service';
@Component({
  selector: 'app-player-pagamento',
  templateUrl: './player-pagamento.component.html',
  styleUrls: ['./player-pagamento.component.scss']
})
export class PlayerPagamentoComponent implements OnInit {

  bsRemoverEndereco = new BehaviorSubject<any>(undefined);
  bsAlterarEndereco = new BehaviorSubject<any>(undefined);
  bsPagamento = new BehaviorSubject<any>(undefined);

  localStorageUtils = new LocalStorageUtils();

  public obj: any;
  optPag: any;
  total: any;
  optGrupoSel = '1';
  valorOriginal = '';
  taxaPorBoleto: number = 1.99;

  idSolicitacao: number;
  idProduto: number;

  listEnderecos = new Array();

  constructor(
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private enderecoService: EnderecoService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    public dialog: MatDialog,
    public direcionamento: DirecionamentoService,
    public pagamentoService: PagamentoService
  ) { }

  async ngOnInit() {
    this.idSolicitacao = this.activatedRoute.snapshot.params.idSolicitacao;
    this.idProduto = this.activatedRoute.snapshot.params.idProduto;

    await this.direcionamento.direcionarStatusSolicitacao('player-pagamento', this.idSolicitacao);

    let dadosUsuario = this.localStorageUtils.lerUsuario();
    
    this.obj = {
      solicitacao: this.idSolicitacao,
      usuario: dadosUsuario && dadosUsuario.nome ? dadosUsuario.nome : '',
      produto: '',
      valorTotal: 0
    };
    
    await this.buscarValorTotalSolicitacao();

    try {
      let taxacaoBoleto = await this.pagamentoService.BuscarTaxaPorBoleto().toPromise();
      if (taxacaoBoleto) {
        this.taxaPorBoleto = taxacaoBoleto.taxaBoleto
      }
    } catch (e) {
      console.log(e);
      this.toastr.error(`Ocorreu um erro ao buscar a taxa do boleto.`, 'Tabelionet', ToastOptions);
    }
    
    

    if (!this.obj && this.idSolicitacao) {
      this.router.navigate([`/carrinho/${this.idSolicitacao}`]);
    } else if (!this.obj) {
      this.router.navigate([`/home`]);
    } else if (this.obj.solicitacao !== this.idSolicitacao) {
      this.router.navigate([`/carrinho/${this.idSolicitacao}`]);
    }
  }

  buscarValorTotalSolicitacao() {
    this.pagamentoService.BuscarValorTotalSolicitacao(this.idSolicitacao)
      .subscribe((composicao) => {
        if (composicao) {
          this.obj.valorTotal = composicao.valorTotal;
          this.valorOriginal = composicao.valorTotal;
          this.obj.produto = composicao.tituloProduto;
        } else {
          this.toastr.error(`Não foi possível buscar a composição dos preços da solicitação.`, 'Tabelionet', ToastOptions);
        }
      },
      (err) => {
        this.toastr.error(`Ocorreu um erro ao buscar a composição dos preços da solicitação.\n\n{err.message}`, 'Tabelionet', ToastOptions);
      })
  }

  AlterarValorTotal(tipoPagamento: string){    
    if(tipoPagamento === 'boleto'){      
      this.obj.valorTotal = (Number.parseFloat(this.valorOriginal) + this.taxaPorBoleto).toString();
    }
    else{
      this.obj.valorTotal = this.valorOriginal;
    }
    
  }
}
