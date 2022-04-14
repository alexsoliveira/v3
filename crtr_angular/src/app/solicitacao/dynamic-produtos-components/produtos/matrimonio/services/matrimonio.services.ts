
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { BaseService } from 'src/app/services/base.service';
import { Router } from '@angular/router';
import { DadosMatrimonioDto } from '../models/dto/dados-matrimonio-dto.model';

@Injectable({
  providedIn: 'root'
})
export class MatrimonioService extends BaseService {

  constructor(private http: HttpClient, public router: Router) { super(router); }

  AdicionarMatrimonio(dadosMatrimonio: DadosMatrimonioDto): Observable<any> {
    console.log(super.ObterAuthHeaderJson);
    return this.http
      .post(this.UrlService + '/Solicitacoes/CriarMatrimonio', dadosMatrimonio, super.ObterAuthHeaderJson())
      .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }

  BuscarDadosMatrimonio(idSolicitacao: number): Observable<any> {
    return this.http
    .get(this.UrlService + `/Produtos/BuscarDadosMatrimonio/${idSolicitacao}`, super.ObterAuthHeaderJson())
    .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }
}
