import { Component, OnInit, AfterContentInit, Input, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgBrazilValidators } from 'ng-brazil';
import { utilsBr } from 'js-brasil';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ToastOptions } from '../../../utils/toastr.config';
import { ActivatedRoute, Router } from '@angular/router';
import { ValidacoesCC } from '../../../validators/cartaoCredito.validator';
import { MatDialog } from '@angular/material/dialog';
import { ModalComponent } from '../../../shared/modal/modal.component';
import { BehaviorSubject } from 'rxjs';
import { PagamentoService } from '../../services/pagamento.service';
import { PlayerPagamentoComponent } from '../player-pagamento.component';
import { SimuladorParcelas } from '../../models/simuladorParcelas.model';
import { DonoCartao } from './models/dono-cartao.model';
import { CartaoCredito } from './models/cartao-credito.model';
import { Cartao } from './models/cartao.model';
import { DatePipe } from '@angular/common';
import { BlockLike, textChangeRangeIsUnchanged } from 'typescript';
import { WHITE_ON_BLACK_CSS_CLASS } from '@angular/cdk/a11y/high-contrast-mode/high-contrast-mode-detector';

@Component({
  selector: 'app-cartao-credito',
  templateUrl: './cartao-credito.component.html',
  styleUrls: ['./cartao-credito.component.scss']
})
export class CartaoCreditoComponent implements OnInit, AfterContentInit {

  @Input() bsPagamento: BehaviorSubject<any>;

  formGroup: FormGroup;
  MASKS = utilsBr.MASKS;

  parcelamentoValido: boolean = false;
  cartaoCredito: any;
  validadeCartao: any;
  codSeg: any;
  parcelamento: SimuladorParcelas;
  idSolicitacao: number;
  retornouServicoParcelamento: boolean = false;
  retornouServicoVerificacaoPagamento: boolean = false;

  constructor(
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private router: Router,
    private fb: FormBuilder,
    public dialog: MatDialog,
    public pagamentoService: PagamentoService,
    private activatedRoute: ActivatedRoute,
    public datePipe: DatePipe,
    @Inject(PlayerPagamentoComponent) private player: PlayerPagamentoComponent
  ) { }

  ngAfterContentInit(): void {
    this.cartaoCredito = [/\d/, /\d/, /\d/, /\d/, ' ', /\d/, /\d/, /\d/, /\d/, ' ', /\d/, /\d/, /\d/, /\d/, ' ', /\d/, /\d/, /\d/, /\d/];
    this.validadeCartao = [/[0-1]/, /[0-2]/, '/', /[0-2]/, /[0-9]/, /\d/, /\d/];
    this.codSeg = [/[0-9]/, /[0-9]/, /\d/];
  }

  ngOnInit(): void {
    this.formGroup = this.fb.group({

      numeroCartao: [
        '',
        Validators.compose([Validators.required, ValidacoesCC.numeroCartao])
      ],
      nomeCompleto: ['', Validators.required],
      dataVencimento: ['',
        Validators.compose([Validators.required, ValidacoesCC.validadeCartao])
      ],
      codSeg: ['', Validators.compose([Validators.required, ValidacoesCC.cvv])],
      cpf: ['', Validators.compose([Validators.required, NgBrazilValidators.cpf])],
      numeroParcelas: ['', Validators.required]
    });

    this.idSolicitacao = this.activatedRoute.snapshot.params.idSolicitacao;
    this.spinner.show();
    this.verificarSeExistePagamentoJaEfetuado();

    var intervalBuscarParcelamento = setInterval(() => {
      if (this.player.obj.valorTotal 
        && this.player.obj.valorTotal > 0) {
        this.buscarParcelamento();    
        clearInterval(intervalBuscarParcelamento);
      }
    }, 500);
    

    var intervalSpinner = setInterval(() => {
      if (this.retornouServicoParcelamento && this.retornouServicoVerificacaoPagamento) {
        //this.spinner.hide();
        clearInterval(intervalSpinner);
      }
    }, 500);
  }

  get numeroCartao(): any {
    return this.formGroup.get('numeroCartao');
  }
  get dataCartao(): any {
    return this.formGroup.get('dataVencimento');
  }
  get cvvCartao(): any {
    return this.formGroup.get('codSeg');
  }

  Pagar(): void {

    if (!this.isValidVencimentoCartao()) {
      this.toastr.error('A validade do cartão está inválida!', 'Tabelionet', ToastOptions);
      return;
    }

    const dialogRef = this.dialog.open(ModalComponent);
    dialogRef.disableClose = true;

    dialogRef.componentInstance.titulo = "Pagamento";
    dialogRef.componentInstance.conteudo = "Confirma efetuar o pagamento?";
    dialogRef.componentInstance.setBotaoOkIconCheck();

    dialogRef.afterClosed().subscribe(ok => {



      if (ok) {
        let donoCartao = new DonoCartao(this.formGroup.get('nomeCompleto').value);
        let venc = this.getVencimentoCartao();
        let carrinho = JSON.parse(localStorage.getItem('_c'));
        if (!carrinho || !carrinho.valorTotal) {
          this.toastr.error('Ocorreu um erro ao buscar os dados da solicitação!', 'Tabelionet', ToastOptions);
          return;
        }
        let cartao = new Cartao(
          this.formGroup.get('numeroCartao').value,
          venc.mes,
          venc.ano,
          this.formGroup.get('codSeg').value,
          donoCartao
        );

        let parcelaSelecionada = this.parcelamento.parcelas.find((parcela) => parcela.numero == this.formGroup.get('numeroParcelas').value);
        let cartaoCredito = new CartaoCredito(
          parcelaSelecionada.numero,
          parcelaSelecionada.valorTotal,
          cartao,
          this.formGroup.get('cpf').value
        );


        this.spinner.show();

        this.pagamentoService.PagarComCartaoCredito(this.idSolicitacao, cartaoCredito)
          .subscribe((status) => {
            this.spinner.hide();
            if (status === 'DECLINED' || status === 'CANCELED') {
              this.toastr.error(`Não foi possível realizar o pagamento com os dados do cartão informado. 
                               \nVerifique os dados informados ou utilize outro meio de pagamento!`, 'Tabelionet', ToastOptions);
            }
            else if (status === 'AUTHORIZED') {
              localStorage.removeItem('carrinho');
              this.toastr.warning(`O pagamento está em análise pela administradora do seu cartão.
                                 \nFique atento ao seu e-mail, que vamos atualizá-lo quando recebermos uma resposta.`, 'Tabelionet', ToastOptions);
            }
            else if (status === 'PagamentoEfetuadoAnteriormente') {
              localStorage.removeItem('carrinho');
              this.toastr.warning(`O pagamento já foi efetuado anteriormente e consta como aprovado!`, 'Tabelionet', ToastOptions);
            }

            localStorage.removeItem('carrinho');
            this.toastr.success(`O pagamento foi efetuado com sucesso, confira aqui o andamento da sua solicitação!
                                 \n\nO status da sua solicitação também pode ser acessado pela sua conta.`, 'Tabelionet', ToastOptions);
            this.bsPagamento.next(true);
            this.router.navigateByUrl(`/status-solicitacao/${this.idSolicitacao}`);
          },
            (err) => {
              this.spinner.hide();
              this.toastr.error(err.error, 'Tabelionet', ToastOptions);
            })
      }
    });
  }


  buscarParcelamento() {
    let valorTotal = this.player.obj.valorTotal.toString().replace(',', '.');
    this.pagamentoService.BuscarParcelamento(valorTotal)
      .subscribe((parcelamento: any) => {
        this.retornouServicoParcelamento = true;
        if (parcelamento && parcelamento.parcelas && parcelamento.parcelas.length > 0) {
          this.parcelamento = parcelamento;
          this.parcelamentoSelecionado(parcelamento.parcelas[0].numero);
          this.parcelamentoValido = true;
        }
        else {
          this.toastr.error(`Ocorreu um erro ao buscar as opções de parcelamento!`, 'Tabelionet', ToastOptions);
          this.parcelamentoValido = false;
          this.parcelamento = new SimuladorParcelas();
          
          //LIXOO MOCKKK
          // this.parcelamento.mockarParcelas(this.player.obj.valorTotal.replace(',', '.'));
          // this.parcelamentoSelecionado(1);
          // this.parcelamentoValido = true;
          //Certo codigo debaixo
          //TODO: REMOVER CODIGO ACIMA DO TEXTO "LIXO MOCKKK" ATÉ AQUI E SUBSTITUIR PELO DEBAIXO
          this.parcelamento.zeroParcelas();
        }

        this.spinner.hide();
      },
        (err) => {
          this.retornouServicoParcelamento = true;
          if (err.error && err.error.mensagemErro) {
            this.toastr.error(`Ocorreu um erro ao buscar as opções de parcelamento: \n\n${err.error.mensagemErro}`, 'Tabelionet', ToastOptions);
          }
          else {
            this.toastr.error(`Ocorreu um erro ao buscar as opções de parcelamento!`, 'Tabelionet', ToastOptions);
          }

          this.parcelamentoValido = false;
          this.parcelamento = new SimuladorParcelas();
          
          //LIXOO MOCKKK
          // this.parcelamento.mockarParcelas(this.player.obj.valorTotal.replace(',', '.'));
          // this.parcelamentoSelecionado(1);
          // this.parcelamentoValido = true;
          //Certo codigo debaixo
          //TODO: REMOVER CODIGO ACIMA DO TEXTO "LIXO MOCKKK" ATÉ AQUI E SUBSTITUIR PELO DEBAIXO
          this.parcelamento.zeroParcelas();
          this.spinner.hide();
        });
  }

  parcelamentoSelecionado(parcela) {
    this.formGroup.get('numeroParcelas').setValue(parcela);
  }


  isValidVencimentoCartao(): boolean {
    let date = Date.now();
    let now = {
      mes: this.datePipe.transform(date, 'MM'),
      ano: this.datePipe.transform(date, 'yyyy')
    }

    let validade = this.getVencimentoCartao();

    if (!validade) {
      return false;
    }

    if (Number.parseInt(now.ano) > Number.parseInt(validade.ano)) {
      return false;
    }

    if (Number.parseInt(validade.ano) == Number.parseInt(now.ano)) {
      if (Number.parseInt(validade.mes) < Number.parseInt(now.mes)) {
        return false;
      }
    }

    return true;
  }

  getVencimentoCartao(): any {
    let validade = this.formGroup.get('dataVencimento').value.toString().split('/');

    if (!validade || validade.length < 2) {
      return undefined;
    }

    return {
      mes: validade[0],
      ano: validade[1]
    };
  }


  verificarSeExistePagamentoJaEfetuado(): void {
    this.pagamentoService.ConsultarTransacao(this.idSolicitacao)
      .subscribe((obj) => {
        if (obj.status) {
          if (obj.status == 'AUTHORIZED') {
            this.toastr.warning(`Já existe um pagamento em andamento para esta solicitação!`, 'Tabelionet', ToastOptions);
            this.router.navigateByUrl(`/status-solicitacao/${this.idSolicitacao}`);
          }
          if (obj.status == 'PAID') {
            this.toastr.success(`Esta solicitação já possui um pagamento válido!`, 'Tabelionet', ToastOptions);
            this.router.navigateByUrl(`/status-solicitacao/${this.idSolicitacao}`);
          }
        }

        this.retornouServicoVerificacaoPagamento = true;
      },
        (err) => {
          this.retornouServicoVerificacaoPagamento = true;
          this.toastr.error(`Ocorreu um erro ao verificar se já existe pagamento para esta solicitação!`, 'Tabelionet', ToastOptions);
        })
  }
}
