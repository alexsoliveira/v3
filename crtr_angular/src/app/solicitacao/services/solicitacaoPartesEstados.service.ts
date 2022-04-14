import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseService } from '../../services/base.service';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class SolicitacaoPartesEstadosService extends BaseService {

constructor(
  private http: HttpClient, public router: Router) { super(router); }

BuscarTodos(): any{
  return this.http
  .get(this.UrlService + '/SolicitacoesPartesEstadosPc/BuscarTodos', super.ObterAuthHeaderJson())
  .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
}

}
