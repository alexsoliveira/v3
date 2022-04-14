import { PatternInteracaoValidator } from './../../../../../validators/pattern-interacao.validator';
import { TipoDocumento } from './../../../../models/tipoDocumento.model';
import { TestemunhasDataGrid } from './../models/testemunhasDatagrid';
import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { HtmlSelect } from 'src/app/shared/services/HtmlSelect.service';

import { TestemunhaCadastro } from './models/testemunha-cadastro.model';
import { DatagridTestemunhaComponent } from '../datagrid-testemunha/datagrid-testemunha.component';
import { TestemunhaDto } from '../models/dto/testemunha-dto.model';

@Component({
  selector: 'app-cadastro-testemunha',
  templateUrl: './cadastro-testemunha.component.html',
  styleUrls: ['./cadastro-testemunha.component.scss']
})
export class CadastroTestemunhaComponent implements OnInit {

  @Input() inputDatagridTestemunha: TestemunhaDto[] = [];
  selectTipoDocumentos: any[] = [];
  tipoDocumentoTestemunha = new FormControl();
  selectParte: any[] = [];
  parte = new FormControl();
  formTestemunha: FormGroup;
  testemunhaFormControl = new FormControl('', [
    Validators.required
  ]);

  @Output() atualizarDados = new EventEmitter();

  exibirRg: boolean = true;
  listaDataGridTestemunhas: TestemunhasDataGrid[] = [];
  testemunhaCadastro: TestemunhaCadastro[] = [];

  @ViewChild(DatagridTestemunhaComponent) dataGridTestemunhas;

  constructor(
    private fb: FormBuilder,
    private htmlSelect: HtmlSelect,
    public piv: PatternInteracaoValidator
  ) { }

  ngOnInit() {
    this.selectTipoDocumentos = this.htmlSelect.getTipoDocumento();
    this.selectParte = this.htmlSelect.getParte();

    this.formTestemunha = this.fb.group({
      nomeTestemunha: ['', Validators.required],
      tipoDocumentoTestemunha: ['', Validators.required],
      documentoTestemunha: ['', Validators.required],
      rgTestemunha: [''],
      parte: ['', Validators.required],
    });

    let intervalTestemunhasVindoDoPai = setInterval(() => {
      if (this.inputDatagridTestemunha.length > 0){
        this.dataGridTestemunhas.dataSource = this.inputDatagridTestemunha;

        this.dataGridTestemunhas.updateGridRows();
      }

      clearInterval(intervalTestemunhasVindoDoPai);
    }, 1000)
  }

  adicionarTestemunhas() {
    this.atualizarValidacoesCamposTestemunhas();
    
    if (this.formTestemunha.valid && this.naoInteragiuComControle()) {
      this.addTestemunha(
        this.formTestemunha.get('nomeTestemunha').value,
        this.formTestemunha.get('tipoDocumentoTestemunha').value,
        this.formTestemunha.get('documentoTestemunha').value,
        this.formTestemunha.get('rgTestemunha').value,
        this.formTestemunha.get('parte').value
      );

      this.piv.resetarCampos(this.formTestemunha);
    }
  }

  atualizarValidacoesCamposTestemunhas(): void {
    this.validarTestemunhas();
  }

  validarTestemunhas(): void {
    if (this.exibirRg) {
      this.limparCamposTestemunhasNacional();
    } else {
      this.limparCamposTestemunhasInternacional();
    }
    this.piv.atualizarValidacoesCampos(this.formTestemunha);
  }

  limparCamposTestemunhasNacional(): void {
    this.formTestemunha.get('nomeTestemunha').setValidators(Validators.required);
    this.formTestemunha.get('tipoDocumentoTestemunha').setValidators(Validators.required);
    this.formTestemunha.get('documentoTestemunha').setValidators(Validators.required);
    this.formTestemunha.get('rgTestemunha').setValidators(Validators.required);
    this.formTestemunha.get('parte').setValidators(Validators.required);
  }

  limparCamposTestemunhasInternacional(): void {
    this.formTestemunha.get('nomeTestemunha').setValidators(Validators.required);
    this.formTestemunha.get('tipoDocumentoTestemunha').setValidators(Validators.required);
    this.formTestemunha.get('documentoTestemunha').setValidators(Validators.required);
    this.formTestemunha.get('rgTestemunha').setValue('');
    this.formTestemunha.get('rgTestemunha').clearValidators();
    this.formTestemunha.get('parte').setValidators(Validators.required);
  }

  validarTipoDocumento(option: any): void {
    this.exibirRg = this.selectTipoDocumentos.find(p => p.valor == option.value).texto === 'CPF';
  }

  removerTestemunha(Testemunha) {
    this.listaDataGridTestemunhas = this.dataGridTestemunhas.dataSource;
    
    this.testemunhaCadastro = this.testemunhaCadastro.filter((value, key) => {
      return value.DocumentoTestemunha != Testemunha.documento;
    });

    this.dataGridTestemunhas.updateGridRows();
    this.atualizarDados.emit();
  }

  naoInteragiuComControle(): boolean {
    if (!this.formTestemunha.get('nomeTestemunha').value)
      this.formTestemunha.get('nomeTestemunha').setValue('');
    if (!this.formTestemunha.get('documentoTestemunha').value)
      this.formTestemunha.get('documentoTestemunha').setValue('');
    return (this.formTestemunha.get('nomeTestemunha').dirty
      || this.formTestemunha.get('nomeTestemunha').value != '')
      && (this.formTestemunha.get('documentoTestemunha').dirty
        || this.formTestemunha.get('documentoTestemunha').value != '');
  }



  //metodos auxiliares

  addTestemunha(
    nome: string,
    tipoDocumento: any,
    documento: any,
    rg: any,
    parte: any
  ) {

    this.testemunhaCadastro.push({
      NomeTestemunha: nome,
      TipoDocumentoTestemunha: tipoDocumento,
      DocumentoTestemunha: documento,
      RgTestemunha: rg,
      Parte: parte
    });

    this.listaDataGridTestemunhas.push(
      new TestemunhasDataGrid(
        undefined,
        nome,
        tipoDocumento,
        documento,
        rg,
        parte
      )
    );

    this.dataGridTestemunhas.updateGridRows();
    this.atualizarDados.emit();
  }
}
