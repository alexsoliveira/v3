import { Component, OnInit } from '@angular/core';
import { LocalStorageUtils } from '../../../utils/localstorage';

@Component({
  selector: 'app-banner',
  templateUrl: './banner.component.html',
  styleUrls: ['./banner.component.scss']
})
export class BannerComponent implements OnInit {

  constructor() { }

  localstorage: LocalStorageUtils;
  exibirPoliticaPrivacidade: Boolean = false;

  ngOnInit(): void {
    this.localstorage = new LocalStorageUtils();
    
    if (this.localstorage) {
      this.exibirPoliticaPrivacidade = !this.localstorage.isPoliticaPrivacidadeAcordada();
    }
  }

  ativar() {
    this.localstorage = new LocalStorageUtils();
    if (this.localstorage) {
      this.localstorage.setPoliticaPrivacidade();
      this.exibirPoliticaPrivacidade = false;
    } else {
      console.log('Não foi possível carregar LocalStorage!');
    }
  }
}
