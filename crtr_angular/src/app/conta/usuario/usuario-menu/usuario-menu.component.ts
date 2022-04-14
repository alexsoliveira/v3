import { Component, Input, OnInit, ViewChild, ViewChildren } from '@angular/core';
import { LocalStorageUtils } from '../../../utils/localstorage';
import { Router } from '@angular/router';
import { MatMenuTrigger } from '@angular/material/menu/menu-trigger';
import { MatMenuPanel } from '@angular/material/menu';

@Component({
  selector: 'app-usuario-menu',
  templateUrl: './usuario-menu.component.html',
  styleUrls: ['./usuario-menu.component.scss']
})
export class UsuarioMenuComponent implements OnInit {

  localStorageUtils = new LocalStorageUtils();

  @Input() nomeUsuario: string;

  constructor(
    private router: Router
  ) { }

  ngOnInit() {
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
