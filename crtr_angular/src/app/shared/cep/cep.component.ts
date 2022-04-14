import { Component, OnInit, ViewChild, ElementRef, Input, AfterViewInit, AfterContentInit, Output } from '@angular/core';
import { MatProgressButtonOptions } from 'mat-progress-buttons';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ToastOptions } from '../../utils/toastr.config';
import { EnderecoService } from '../services/Endereco.service';
import { Endereco } from '../models/Endereco.model';
import { ValidacoesCEP } from '../../validators/cep.validator';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-cep',
  templateUrl: './cep.component.html',
  styleUrls: ['./cep.component.scss']
})
export class CepComponent implements OnInit {

  @ViewChild('objCep') txtcep: ElementRef;

  constructor(
    private toastr: ToastrService,
    private enderecoService: EnderecoService,
  ) { }

  @Input() tituloInput: boolean = false;
  @Input() habComp: boolean = true;

  flag: boolean = false;
  mascaraCEP: any;
  formgroup: FormGroup;
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

  ngOnInit() {

    this.mascaraCEP = [/\d/, /\d/, /\d/, /\d/, /\d/, '-', /\d/, /\d/, /\d/];

    this.formgroup = new FormGroup({
      cep: new FormControl(
        '',
        [
          Validators.compose([Validators.required, ValidacoesCEP.numero])
        ]
      )
    });

    if (!this.habComp)
      this.formgroup.disable();
  }

  get formatoCEP(): any {
    return this.formgroup.get('cep');
  }

  BuscaEndereco(cep: string): any {

    if (!this.formgroup.valid)
      return;

    cep = cep.replace(/\D+/g, '');

    this.barButtonOptions.active = true;
    //this.formgroup.disable();

    this.enderecoService.BuscarEndereco(cep)
      .subscribe((ret: Endereco) => {

        console.log(ret);

        //this.barButtonOptions.disabled = true;

      },
        (err) => {

          this.flag = true;

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

          //this.formgroup.enable();
          this.barButtonOptions.active = false;

          if (this.flag) {
            this.formgroup.reset();
            this.txtcep.nativeElement.select();
          }
          else {
            //this.txtnumero.nativeElement.value
            //this.txtnumero.nativeElement.focus();
          }

          this.flag = false;
          console.log('Fim');
        });
  }

  EvtBotaoCEP(): void {
    if (!this.formgroup.valid)
      this.barButtonOptions.disabled = true;
    else
      this.barButtonOptions.disabled = false;
  }
}
