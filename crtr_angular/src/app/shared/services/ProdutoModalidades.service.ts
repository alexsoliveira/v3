import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BaseService } from '../../services/base.service';
import { catchError } from 'rxjs/operators';
import { Modalidade } from '../models/Modalidade.model';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class ProdutoModalidadesService extends BaseService {

  constructor(private http: HttpClient, public router: Router) { super(router); }

  obterModalidades(): Observable<Modalidade[]>{
    return this.http
        .get<Modalidade[]>(this.UrlService + '/ProdutosModalidadesPc/BuscarTodos')
        .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }

}


