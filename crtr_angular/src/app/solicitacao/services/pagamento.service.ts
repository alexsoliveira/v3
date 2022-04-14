import { SimuladorParcelas } from '../models/simuladorParcelas.model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseService } from 'src/app/services/base.service';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class PagamentoService extends BaseService {

  constructor(private http: HttpClient, public router: Router) { super(router); }

  BuscarParcelamento(valorTotal: number): Observable<SimuladorParcelas> {
    return this.http
      .post(this.UrlService + `/Pagamento/SimularParcelamento/${valorTotal}`, super.ObterAuthHeaderJson())
      .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }

  ConsultarTransacao(idSolicitacao: number): Observable<any> {
    return this.http
      .get(this.UrlService + `/Pagamento/ConsultarTransacaoCartaoCredito/${idSolicitacao}`, super.ObterAuthHeaderJson())
      .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }

  PagarComCartaoCredito(idSolicitacao: number, dadosPagamento: any): Observable<string> {
    return this.http
      .post(this.UrlService + `/Pagamento/PagarComCartaoCredito/${idSolicitacao}`, dadosPagamento, 
          super.ObterAuthHeaderJson())
          .pipe(catchError((res: Response) => {
            return super.serviceErrorNavigate(res, this.router);
          }));
  }

  PagarViaBoleto(idSolicitacao: number, dadosPagamento: any): Observable<any> {
    return this.http
      .post(this.UrlService + `/Pagamento/PagarViaBoleto/${idSolicitacao}`, dadosPagamento, 
          super.ObterAuthHeaderJson())
          .pipe(catchError((res: Response) => {
            return super.serviceErrorNavigate(res, this.router);
          }));
  }

  ConsultarPagamentoBoleto(idSolicitacao: number): Observable<any> {
    return this.http
      .get(this.UrlService + `/Pagamento/ConsultarPagamentoBoleto/${idSolicitacao}`, super.ObterAuthHeaderJson())
      .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }

  BuscarValorTotalSolicitacao(idSolicitacao: number): Observable<any> {
    return this.http
      .get(this.UrlService + `/Taxas/BuscarComposicaoPrecoProdutoTotal/${idSolicitacao}`, super.ObterAuthHeaderJson())
      .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }

  BuscarTaxaPorBoleto(): Observable<any> {
    return this.http
      .post(this.UrlService + `/Pagamento/ConsultarTaxaBoleto`, super.ObterAuthHeaderJson())
      .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }

}
