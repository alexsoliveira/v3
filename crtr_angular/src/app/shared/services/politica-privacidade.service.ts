import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Endereco } from '../models/Endereco.model';
import { NovoEndereco } from '../models/NovoEndereco.model';
import { BaseService } from '../../services/base.service';
import { catchError } from 'rxjs/operators';
import { number } from 'ng-brazil/number/validator';
import { Router } from '@angular/router';
import { TermoConcordancia } from 'src/app/solicitacao/pagamento/carrinho/models/termoConcordancia.model';

@Injectable({
  providedIn: 'root'
})
export class PoliticaPrivacidadeService extends BaseService {

  constructor(private http: HttpClient, public router: Router) { super(router); }

  ObterTermosUso(): Observable<TermoConcordancia>{
    return this.http
      .post<TermoConcordancia>(this.UrlService + '/Carrinho/ObterTermoConcordancia?descricao=TermoUso', super.ObterAuthHeaderJson())
      .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }

}


