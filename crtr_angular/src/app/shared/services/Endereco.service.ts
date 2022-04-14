import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Endereco } from '../models/Endereco.model';
import { NovoEndereco } from '../models/NovoEndereco.model';
import { BaseService } from '../../services/base.service';
import { catchError } from 'rxjs/operators';
import { number } from 'ng-brazil/number/validator';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class EnderecoService extends BaseService {

  constructor(private http: HttpClient, public router: Router) { super(router); }

  BuscarEndereco(cep: string): Observable<Endereco> {
    return this.http
      .post(this.UrlService + `/Enderecos/Buscar?cep=${cep}`, super.ObterAuthHeaderJson())
      .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }
  BuscarEnderecos(idUsuario: number): Observable<Endereco> {
    return this.http
      .get(this.UrlService + '/Enderecos/BuscarTodosPorUsuario/' + idUsuario, super.ObterAuthHeaderJson())
      .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }
  IncluirEndereco(endereco: NovoEndereco): Observable<Endereco> {
    return this.http
      .post(this.UrlService + '/Enderecos/IncluirEndereco/', endereco, super.ObterAuthHeaderJson())
      .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }
  AlterarEndereco(endereco: Endereco): Observable<Endereco> {
    return this.http
      .put(this.UrlService + '/Enderecos/AtualizarEndereco/', endereco, super.ObterAuthHeaderJson())
      .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }
  ApagarEndereco(idEndereco: any): Observable<any> {
    return this.http
      .delete(this.UrlService + '/Enderecos/ApagarEndereco/' + idEndereco, super.ObterAuthHeaderJson())
      .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }
  AtualizarEnderecoPrincipal(idEndereco: any): Observable<any> {
    return this.http
      .post(this.UrlService + `/Enderecos/AtualizarEnderecoPrincipal/${idEndereco}`, super.ObterAuthHeaderJson())
      .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }

}


