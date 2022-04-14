import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, Validators, FormControl, FormBuilder } from '@angular/forms';
import { NgBrazilValidators } from 'ng-brazil';
import { utilsBr } from 'js-brasil';
import { BehaviorSubject } from 'rxjs';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastOptions } from '../../../utils/toastr.config';
import { ModalComponent } from '../../../shared/modal/modal.component';
import { MatDialog } from '@angular/material/dialog';
import { SolicitacaoService } from '../../services/solicitacao.service';
import { PagamentoService } from '../../services/pagamento.service';
import { Boleto } from './models/boleto-bancario.model';
import { Pagador } from './models/pagador.model';
import { BoletoUtils } from 'src/app/utils/boleto.utils';

@Component({
  selector: 'app-boleto-bancario',
  templateUrl: './boleto-bancario.component.html',
  styleUrls: ['./boleto-bancario.component.scss']
})
export class BoletoBancarioComponent implements OnInit {

  @Input() bsPagamento: BehaviorSubject<any>;

  formGroup: FormGroup;
  MASKS = utilsBr.MASKS;

  tipoDocumentos: any[] = [
    { valor: 2, texto: 'CPF' },
    { valor: 5, texto: 'CNPJ' }
  ];
  
  idSolicitacao: number;
  retornouServicoVerificacaoPagamento: boolean = false;

  constructor(
    private solicitacaoService: SolicitacaoService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private router: Router,
    private fb: FormBuilder,
    public dialog: MatDialog,
    public pagamentoService: PagamentoService,
    private activatedRoute: ActivatedRoute,
    private boletoUtils: BoletoUtils
  ) { }

  ngOnInit(): void {
    this.formGroup = this.fb.group({
      tipoDocumento: ['', Validators.required],
      numero: [
        '',
        Validators.compose([Validators.required, NgBrazilValidators.cpf])
      ],
      nomeCompleto: ['', Validators.required]
    });

    this.formGroup.patchValue({ tipoDocumento: 2 });
    this.idSolicitacao = this.activatedRoute.snapshot.params.idSolicitacao;

    this.verificarSeExistePagamentoJaEfetuado();
    var intervalSpinner = setInterval(() => {
      if (this.retornouServicoVerificacaoPagamento) {
        //this.spinner.hide();
        clearInterval(intervalSpinner);
      }
    }, 500);
  }

  tipoDoc(tipo: any): void {
    this.formGroup.get('numero').reset();
    this.formGroup.get('nomeCompleto').reset();
    this.formGroup.get('numero').clearValidators();

    switch (tipo) {
      case 2:
        this.formGroup.get('numero').setValidators(Validators.compose([Validators.required, NgBrazilValidators.cpf]));
        break;
      case 5:
        this.formGroup.get('numero').setValidators(Validators.compose([Validators.required, NgBrazilValidators.cnpj]));
        break;
    }
  }

  Pagar(): void {    
    const dialogRef = this.dialog.open(ModalComponent);
    dialogRef.disableClose = true;

    dialogRef.componentInstance.titulo = "Pagamento";
    dialogRef.componentInstance.conteudo = "Confirma efetuar o pagamento?";
    dialogRef.componentInstance.setBotaoOkIconCheck();
    
    dialogRef.afterClosed().subscribe(ok => {

      if(ok){        
        
        let mensagem = "Boleto gerado pelo Tabelionet";
        let carrinho = JSON.parse(localStorage.getItem('_c'));        
        let pagador = new Pagador(          
          this.formGroup.get('nomeCompleto').value,
          this.formGroup.get('numero').value,          
        );        
                
        if(!carrinho || !carrinho.valorTotal) {
          this.toastr.error('Ocorreu um erro ao buscar os dados da solicitação!', 'Tabelionet', ToastOptions);
          return;
        }

        let boleto = new Boleto(          
          mensagem,
          carrinho.valorTotal,
          "",
          pagador,          
        );        

        this.spinner.show();

        this.pagamentoService.PagarViaBoleto(this.idSolicitacao, boleto)
          .subscribe((pagamento) => {
            this.spinner.hide();
              
            this.toastr.success('Boleto gerado com sucesso! Clique aqui ou aguarde até ser redirecionado para visualizar o status da solicitação.', 'Tabelionet', ToastOptions)
              .onHidden.pipe()
              .subscribe(() => this.router.navigateByUrl(`/status-solicitacao/${this.idSolicitacao}`));

              if (pagamento.identificador) {
                let linkBoleto = this.boletoUtils.getLinkBoleto(pagamento.identificador);
    
                window.open(
                  linkBoleto,
                  "_blank",
                  'noopener'
                );            
              } else {
                this.toastr.error(`Boleto não localizado`, 'Tabelionet', ToastOptions);    
              }  

            this.bsPagamento.next(true);
                        
          },
          (err) => {
            this.spinner.hide();
            this.toastr.error(err.error, 'Tabelionet', ToastOptions);
          })        
      }
    });
  }

  verificarSeExistePagamentoJaEfetuado(): void {
    this.pagamentoService.ConsultarPagamentoBoleto(this.idSolicitacao)
      .subscribe((obj) => {
        if (obj && obj.status && obj.status !== 'V') {          
          if (obj.status === 'P' && obj.status === 'B') {
            this.toastr.success(`Esta solicitação já possui um pagamento válido!`, 'Tabelionet', ToastOptions);
            this.router.navigateByUrl(`/status-solicitacao/${this.idSolicitacao}`);
          }
          else // obj.status === 'A' && obj.status === 'R'
          {
            this.toastr.warning(`Já existe um boleto criado para está solicitação!\n\nPara reemitir o boleto, acesse no final da página.`, 'Tabelionet', ToastOptions);
            this.router.navigateByUrl(`/usuario-conta`);
          }
        }

        this.retornouServicoVerificacaoPagamento = true;
      },
        (err) => {
          this.retornouServicoVerificacaoPagamento = true;
          this.toastr.error(`Ocorreu um erro ao verificar se já existe pagamento para está solicitação!`, 'Tabelionet', ToastOptions);
          this.router.navigateByUrl(`/usuario-conta`);
        })
  }

}
