import { Component, OnInit, ViewChild, ElementRef, Inject } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { utilsBr } from 'js-brasil';
import { MatProgressButtonOptions } from 'mat-progress-buttons';
import { EnderecoService } from '../services/Endereco.service';
import { Endereco } from '../models/Endereco.model';
import { NovoEndereco } from '../models/NovoEndereco.model';
import { EnderecoConteudo } from '../models/EnderecoConteudo.model';
import { ToastOptions } from '../../utils/toastr.config';
import { ValidacoesCEP } from '../../validators/cep.validator';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { LocalStorageUtils } from 'src/app/utils/localstorage';

@Component({
  selector: 'app-endereco-novo',
  templateUrl: './endereco-novo.component.html',
  styleUrls: ['./endereco-novo.component.scss']
})
export class EnderecoNovoComponent implements OnInit {

  titulo: string = "Novo endereço";

  public dadosUsuarioLogado: boolean;


  localStorageUtils = new LocalStorageUtils();

  formgroup: FormGroup;
  formGroupCEP: FormGroup;
  MASKS = utilsBr.MASKS;
  flag: boolean = false;
  mascaraCEP: any;

  enderecoEdit: Endereco;

  @ViewChild('txtNumero') txtnumero: ElementRef;
  @ViewChild('objCep') txtcep: ElementRef;

  barButtonOptions: MatProgressButtonOptions = {
    active: false,
    text: 'Buscar',
    buttonColor: 'primary',
    barColor: 'primary',
    raised: true,
    stroked: false,
    mode: 'indeterminate',
    value: 0,
    disabled: false,
    fullWidth: false,
    buttonIcon: {
      fontIcon: 'find_in_page'
    }
  };

  constructor(
    private enderecoService: EnderecoService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private router: Router,
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<EnderecoNovoComponent>,
    @Inject(MAT_DIALOG_DATA) public dadosEndereco: { objEndereco: Endereco }
  ) { }

  get formatoCEP(): any {
    return this.formGroupCEP.get('cep');
  }

  ngOnInit() {

    this.mascaraCEP = [/\d/, /\d/, /\d/, /\d/, /\d/, '-', /\d/, /\d/, /\d/];

    this.formgroup = this.fb.group({
      logradouro: ['', Validators.required],
      numero: ['', Validators.required],
      complemento: [''],
      bairro: ['', Validators.required],
      localidade: ['', Validators.required],
      uf: ['', Validators.required]
    });

    this.formGroupCEP = new FormGroup({
      cep: new FormControl('', [Validators.compose([Validators.required, ValidacoesCEP.numero])])
    });

    //Editar endereço
    if (this.dadosEndereco && !this.dadosEndereco.objEndereco.novoEndereco) {

      this.enderecoEdit = this.dadosEndereco.objEndereco;
      let enderecoJson = this.enderecoEdit.conteudo;

      if (enderecoJson && enderecoJson.cep) {
        this.formGroupCEP.setValue(
          {
            cep: enderecoJson.cep
          });

        this.formgroup.setValue(
          {
            logradouro: enderecoJson.logradouro,
            numero: enderecoJson.numero,
            complemento: enderecoJson.complemento,
            bairro: enderecoJson.bairro,
            localidade: enderecoJson.localidade,
            uf: enderecoJson.uf
          });
      }

    }
  }
  BuscaEndereco(cep: string): any {

    cep = cep.replace(/\D+/g, '');

    this.barButtonOptions.active = true;
    this.formgroup.disable();

    this.enderecoService.BuscarEndereco(cep)
      .subscribe((ret: Endereco) => {

        this.formgroup.setValue(
          {
            logradouro: ret.logradouro,
            bairro: ret.bairro,
            localidade: ret.localidade,
            uf: ret.uf,
            numero: this.formgroup.get('numero').value,
            complemento: this.formgroup.get('complemento').value
          }
        );

        this.barButtonOptions.disabled = true;

      },
        (err) => {

          this.flag = true;

          if (err.status === 401) {
            this.dialogRef.close();
          }

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
        }).add(() => {

          this.formgroup.enable();
          this.barButtonOptions.active = false;

          if (this.flag) {
            this.formgroup.reset();
            this.txtcep.nativeElement.select();
          }
          else {
            //this.txtnumero.nativeElement.value
            this.txtnumero.nativeElement.focus();
          }

          this.flag = false;
          console.log('Fim');
        });
  }
  Salvar(): void {

    this.spinner.show();

    let endereco = new NovoEndereco();
    endereco.conteudo = new EnderecoConteudo();
    Object.assign(endereco.conteudo, this.formgroup.getRawValue());
    Object.assign(endereco.conteudo, this.formGroupCEP.getRawValue());

    //Novo Endereco
    if (this.dadosEndereco && this.dadosEndereco.objEndereco.novoEndereco) {

      if (this.dadosEndereco.objEndereco.enderecoUsuarioLogado) {
        endereco.idUsuario = this.localStorageUtils.lerUsuario().idUsuario;
        endereco.idPessoa = this.localStorageUtils.lerUsuario().idPessoa;
        this.NovoEndereco(endereco);
      }
      else {
        this.toastr.success("Endereço criado com sucesso!", 'Tabelionet', ToastOptions);
        this.spinner.hide();
        this.dialogRef.close(endereco);
      }

    }
    //Alterar endereço
    else {
      Object.assign(this.enderecoEdit, this.formgroup.getRawValue());
      Object.assign(this.enderecoEdit, this.formGroupCEP.getRawValue());
      this.enderecoEdit.idUsuario = this.localStorageUtils.lerUsuario().idUsuario;
      this.enderecoEdit.conteudo = JSON.stringify(endereco);

      this.AlterarEndereco(this.enderecoEdit);
    }
  }
  NovoEndereco(endereco: NovoEndereco): void {
    this.spinner.show();
    this.enderecoService.IncluirEndereco(endereco)
      .subscribe(ret => {
        this.dialogRef.close(ret);
      },

        (err) => {

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
          this.toastr.success("Endereço criado com sucesso!", 'Tabelionet', ToastOptions);
        }).add(() => {
          this.spinner.hide();
        });
  }
  AlterarEndereco(endereco: Endereco): void {
    this.spinner.show();
    this.enderecoService.AlterarEndereco(endereco)
      .subscribe(ret => {
        this.dialogRef.close(ret);
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
          this.toastr.success("Endereço alterado com sucesso!", 'Tabelionet', ToastOptions);
        }).add(() => {
          this.spinner.hide();
        });
  }
  EvtSalvar(v: boolean): void {
    if (v) {
      this.Salvar();
    }
  }
  EvtBotaoCEP(): void {
    console.log(this.formgroup.get('numero').hasError('required'));
    if (!this.formGroupCEP.valid || !this.formgroup.get('numero').hasError('required'))
      this.barButtonOptions.disabled = true;
    else
      this.barButtonOptions.disabled = false;
  }
}
