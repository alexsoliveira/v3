import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { Certificados } from 'src/app/shared/certificado/models/certificados.model';
import { BaseService } from 'src/app/services/base.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class SolicitacaoAssinaturaService extends BaseService {

  constructor(private http: HttpClient, public router: Router) { super(router); }

  // assinar documentos
  assinarPrimeiroPasso(documento: Certificados): Observable<any> {
    return this.http
        .post<any>(`${this.UrlService}/AssinaturaDigital/AssinarPrimeiroPasso`,
        documento, super.ObterAuthHeaderJson())
        .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }

  assinarSegundoPasso(dadosAssinatura: any): Observable<any> {
    return this.http
        .post<any>(`${this.UrlService}/AssinaturaDigital/AssinarSegundoPasso`,
            dadosAssinatura, super.ObterAuthHeaderJson())
        .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }

  ValidacaoDocumento(idMatrimonioDocumento: number): Observable<any> {
    return this.http
        .post<any>(`${this.UrlService}/AssinaturaDigital/ValidacaoDocumento/${idMatrimonioDocumento}`,
         super.ObterAuthHeaderJson())
        .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }

  atualizarStatusSolicitacao(idSolicitacao: number): Observable<boolean> {
    return this.http
      .post(this.UrlService + `/Solicitacoes/AtualizarSolicitacaoParaCarrinho/${idSolicitacao}`, super.ObterAuthHeaderJson())
      .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }

}
