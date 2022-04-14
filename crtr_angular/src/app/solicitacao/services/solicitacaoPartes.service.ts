import { Injectable } from '@angular/core';
import { BaseService } from '../../services/base.service';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class SolicitacaoPartesService extends BaseService {

  constructor(
    private http: HttpClient, public router: Router) { super(router); }

  BuscarStatusSolicitacaoPartes(idSolicitacao: any): any {
    return this.http
      .get(this.UrlService + '/SolicitacoesPartes/BuscarStatusSolicitacaoPartes/' + idSolicitacao, super.ObterAuthHeaderJson())
      .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }

}
