import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-informacoes-importantes',
  templateUrl: './informacoes-importantes.component.html',
  styleUrls: ['./informacoes-importantes.component.scss']
})
export class InformacoesImportantesComponent implements OnInit {

  informacoesImportantesForm: FormGroup;

  @Output() dadosInformacoes = new EventEmitter();

  constructor(
    private fb: FormBuilder
  ) { }

  ngOnInit() {
    this.informacoesImportantesForm = this.fb.group({
      informacoesImportantes: ['']
    });
  }

  pushInformacoesImportantes(): void {
    this.dadosInformacoes.emit(
      this.informacoesImportantesForm.get('informacoesImportantes').value
    );
  }

  setInformacoes(informacoes: string){
    this.informacoesImportantesForm.get('informacoesImportantes').setValue(informacoes);
  }

}
