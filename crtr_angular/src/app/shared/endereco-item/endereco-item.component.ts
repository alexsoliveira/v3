import { EnderecoNovoComponent } from './../endereco-novo/endereco-novo.component';
import { Component, Input, OnInit, ViewChild, AfterContentInit, ChangeDetectorRef } from '@angular/core';
import { Endereco } from '../models/Endereco.model';
import { EnderecoService } from '../services/Endereco.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { MatDialog } from '@angular/material/dialog';
import { ToastOptions } from '../../utils/toastr.config';
import { ModalComponent } from '../modal/modal.component';
import { BehaviorSubject } from 'rxjs';
import { MatRadioButton } from '@angular/material/radio';
import { NovoEndereco } from '../models/NovoEndereco.model';

@Component({
  selector: 'app-endereco-item',
  templateUrl: './endereco-item.component.html',
  styleUrls: ['./endereco-item.component.scss']
})
export class EnderecoItemComponent implements OnInit, AfterContentInit {

  @Input() bsRemoverEndereco: BehaviorSubject<any>;
  @Input() bsAlterarEndereco: BehaviorSubject<any>;
  @Input() bsSelecionarEndereco: BehaviorSubject<any>;

  @Input() objEnd: NovoEndereco;
  @Input() indice: number;

  idEnderecoSelecionado: number;

  @ViewChild('opt') optSel: MatRadioButton;

  constructor(
    private enderecoService: EnderecoService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    public dialog: MatDialog,
    private cd: ChangeDetectorRef
  ) { }

  ngAfterContentInit(): void {
    setTimeout(() => {
      if (this.objEnd.novoEndereco) {
        this.optSel.checked = true;
      }
    }, 100);
  }

  ngOnInit() {
    this.cd.detectChanges();
  }

  alterarEndereco(endereco: NovoEndereco): any {
    const dialogRef = this.dialog.open(EnderecoNovoComponent, { data: { objEndereco: endereco } });
    dialogRef.componentInstance.titulo = "Alterar endereço";
    dialogRef.disableClose = true;

    dialogRef.afterClosed().subscribe(item => {
      if(item !== undefined){
        this.bsAlterarEndereco.next(item);
      }
    });
  }

  apagarEndereco(idEndereco: any): any {

    const dialogRef = this.dialog.open(ModalComponent);
    dialogRef.disableClose = true;

    dialogRef.componentInstance.titulo = "Apagar endereço";
    dialogRef.componentInstance.conteudo = "Confirma apagar o endereço selecionado?";
    dialogRef.componentInstance.setBotaoOkIconCheck();
    
    dialogRef.afterClosed().subscribe(ok => {

      if (ok) {

        this.spinner.show();

        this.optSel.checked = false;

        this.enderecoService.ApagarEndereco(idEndereco)
          .subscribe(ret => {
            this.bsRemoverEndereco.next(idEndereco);
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
            () => {
              this.toastr.success("Endereço apagado com sucesso!", 'Tabelionet', ToastOptions);
            }).add(() => {
              this.spinner.hide();
            });
      }
    });
  }

  getEnderecoSelecionado() : any {
    return this.idEnderecoSelecionado;
  }

  setEnderecoSelecionado(endereco: NovoEndereco, event: any) : any {
    this.objEnd.flagAtivo = true;
    this.bsSelecionarEndereco.next(endereco.idEndereco);
  }

}
