import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { BaseService } from 'src/app/services/base.service';
import { Solicitante } from '../../models/Solicitante.model';
import { Produto } from './../models/produto.model';
import { TermoConcordancia } from './../models/termoConcordancia.model';
import { ComposicaoPreco } from './../models/ComposicaoPreco.model';
import { Participante } from './../models/participante.model';
import { Taxa } from './../models/taxa.model';


@Injectable({
  providedIn: 'root'
})
export class CarrinhoService extends BaseService {

  constructor(
    private http: HttpClient,
    public router: Router
  ) { super(router); }

  BuscarSolicitante(idSolicitacao: number): Observable<Solicitante> {
    return this.http
      .get<Solicitante>(this.UrlService + '/Carrinho/BuscarSolicitante/' + idSolicitacao, super.ObterAuthHeaderJson())
      .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }

  ObterProduto(idSolicitacao: number): Observable<Produto>{
    return this.http
      .get<Produto>(this.UrlService + '/Carrinho/ObterProduto/' + idSolicitacao, super.ObterAuthHeaderJson())
      .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }

  ObterTaxaEmolumentos(idSolicitacao: number): Observable<Taxa>{
    return this.http
    .get<Taxa>(this.UrlService + `/Taxas/BuscarTaxasPorSolicitacao/${idSolicitacao}`, super.ObterAuthHeaderJson())
    .pipe(catchError((res: Response) => {
      return super.serviceErrorNavigate(res, this.router);
    }));
  }

  ObterParticipantes(idSolicitacao: number): Observable<Participante[]>{
    return this.http
      .get<Participante[]>(this.UrlService + '/Carrinho/ObterParticipantes/' + idSolicitacao, super.ObterAuthHeaderJson())
      .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }

  ObterComposicaoPrecos(idSolicitacao: number): Observable<ComposicaoPreco[]>{
    return this.http
      .get<ComposicaoPreco[]>(this.UrlService + '/Carrinho/ObterComposicaoPrecos/' + idSolicitacao, super.ObterAuthHeaderJson())
      .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }

  ObterTermoConcordancia(descricao: string): Observable<TermoConcordancia>{
    return this.http
      .post<TermoConcordancia>(this.UrlService + '/Carrinho/ObterTermoConcordancia?descricao=' + descricao, super.ObterAuthHeaderJson())
      .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }

  AceiteTermoConcordancia(idSolicitacao: number,isTermoAceito:boolean): Observable<any>{
    return this.http
      .post<any>(this.UrlService + '/Carrinho/AceiteTermoConcordancia?id=' + idSolicitacao + '&isTermoAceito=' + isTermoAceito, super.ObterAuthHeaderJson())
      .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }

}

