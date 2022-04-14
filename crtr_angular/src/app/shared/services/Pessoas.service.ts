import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { BaseService } from 'src/app/services/base.service';

@Injectable({
  providedIn: 'root'
})

export class PessoasService extends BaseService {
    constructor(private http: HttpClient, public router: Router) { super(router); }

    PessoaExiste(idTipoDocumento:number, doc: number): Observable<number> {
        return this.http
          .get<number>(this.UrlService + `/pessoas/PessoaExiste/${idTipoDocumento}/${doc}`, super.ObterAuthHeaderJson())
          .pipe(catchError((res: Response) => {
            return super.serviceErrorNavigate(res, this.router);
          }));
      }
}