import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { LocalStorageUtils } from './../../utils/localstorage';

@Component({
  selector: 'app-top-menu',
  templateUrl: './top-menu.component.html',
  styleUrls: ['./top-menu.component.scss']
})
export class TopMenuComponent implements OnInit {

  localStorageUtils = new LocalStorageUtils();

  token: string;
  nome: string;
  esconderBotao: boolean = false;

  constructor(
    private router: Router
  ) { }

  ngOnInit(): void {
    this.usuarioLogado();
  }

  usuarioLogado(): boolean {
    this.token = this.localStorageUtils.lerToken();

    const usuario = this.localStorageUtils.lerUsuario();
    if (usuario != null) {
      this.nome = usuario.nome;
      this.esconderBotao = true
    }

    return this.token !== null;
  }

  logout(): void {

    //TODO: Verificar se faz necessário remover todos os itens da
     //memória local ou individualizados

    this.localStorageUtils.remover();
    //this.localStorageUtils.removerToken();
    //this.localStorageUtils.removerUsuario();
    this.router.navigate(['/']);
  }



}
