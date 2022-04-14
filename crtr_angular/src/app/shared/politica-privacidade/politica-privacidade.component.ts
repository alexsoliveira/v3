import { Component, OnInit } from '@angular/core';
import { PoliticaPrivacidadeService } from '../services/politica-privacidade.service'
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-politica-privacidade',
  templateUrl: './politica-privacidade.component.html',
  styleUrls: ['./politica-privacidade.component.scss']
})
export class PoliticaPrivacidadeComponent implements OnInit {

  constructor(private politicaPrivacidadeService: PoliticaPrivacidadeService,
    private spinner: NgxSpinnerService) { }

  termosUso: string = '';

  async ngOnInit() {
    this.spinner.show();
    let termoConcordancia = await this.politicaPrivacidadeService.ObterTermosUso().toPromise();
    if (termoConcordancia) {
      this.termosUso = termoConcordancia.conteudo;
    }
    this.spinner.hide();
  }

}
