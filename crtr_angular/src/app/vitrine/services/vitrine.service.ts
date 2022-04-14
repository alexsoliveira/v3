import { Produto } from './../detalhamento/models/produto.model';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { BaseService } from 'src/app/services/base.service';
import { CategoriaProduto } from '../models/categoria-produto.model';
import { ProdutosModalidadeNavigation } from '../models/produtoModalidadeNavigation.model';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class VitrineService extends BaseService {

  constructor(private http: HttpClient, public router: Router) { super(router); }

  // Obter todos os produtos do cartório
  vitrine(): Observable<CategoriaProduto[]> {
    return this.http
      .get<CategoriaProduto[]>(this.UrlService + '/Produtos/BuscarDadosVitrine')
      .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }

  // Obter produto do cartório por id
  obterProduto(id: string): Observable<Produto> {
    return this.http
      .get<Produto>(this.UrlService + '/Produtos/BuscarDetalhesProduto/' + id)
      .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }
}
