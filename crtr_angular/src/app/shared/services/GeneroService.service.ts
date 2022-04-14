import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BaseService } from '../../services/base.service';
import { catchError } from 'rxjs/operators';
import { Genero } from '../models/genero.model';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class GeneroService extends BaseService {

  constructor(private http: HttpClient, public router: Router) { super(router); }

  obterGeneros(): Observable<Genero[]>{
    return this.http
        .get<Genero[]>(this.UrlService + '/GenerosPc/BuscarTodos', super.ObterAuthHeaderJson())
        .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }

}


