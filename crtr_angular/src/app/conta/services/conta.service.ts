import { MinhasSolicitacoesDataGrid } from './../models/minhas-solicitacoes-datagrid.model';

import { BaseService } from '../../services/base.service';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';

import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { UsuarioConfirmaConta, UsuarioConta, UsuarioLogin, UsuarioRegistro, UsuarioResetSenha, UsuarioAlterarSenha } from '../models/ContaViewModel';
import { Usuario } from '../models/usuario.model';
import { Contato } from '../models/contato.model';
import { UsuarioDadosPessoais } from '../models/usuario-dados-pessoais.model';
import { TermoUso } from '../models/termo-uso.model';



@Injectable({
  providedIn: 'root'
})
export class ContaService extends BaseService {

  constructor(
    private http: HttpClient,
    public router: Router
  ) { super(router); }

  Cadastrar(dados: UsuarioRegistro): any {
    return this.http
      .post(this.UrlService + '/Conta/Cadastrar', dados, this.ObterHeaderJson())
      .toPromise();
  }
  Login(dados: UsuarioLogin): any {
    return this.http
      .post(this.UrlService + '/Conta/Login', dados, this.ObterHeaderJson())
      .toPromise();
  }
  EnviarEmailAtivacao(dados: UsuarioConta): any {
    return this.http
    .post(this.UrlService + '/Conta/EnviarEmailAtivacao', dados, this.ObterHeaderJson())
    .toPromise();
  }
  ConfirmarEmailAtivacao(dados: UsuarioConfirmaConta): any {
    return this.http
      .post(this.UrlService + '/Conta/ConfirmarEmailAtivacao', dados, this.ObterHeaderJson())
      .toPromise();
  }
  EnviarEmailResetSenha(dados: UsuarioConta): any{
    return this.http
    .post(this.UrlService + '/Conta/EnviarEmailResetSenha', dados, this.ObterHeaderJson())
    .toPromise();
  }
  ResetarSenha(dados: UsuarioResetSenha): any {
    return this.http
    .post(this.UrlService + '/Conta/ResetarSenha', dados, this.ObterHeaderJson())
    .toPromise();
  }
  AlterarSenha(dados: UsuarioAlterarSenha): Observable<any> {
    return this.http
    .post(this.UrlService + '/Conta/AlterarSenha', dados, this.ObterHeaderJson())
    .pipe(catchError((res: Response) => {
      return super.serviceErrorNavigate(res, this.router);
    }));
  }
  BuscarDadosUsuario(id: number): Observable<Usuario> {
    return this.http
           .get<Usuario>(this.UrlService + '/Conta/BuscarDadosUsuario/' + id, this.ObterHeaderJson())
           .pipe(catchError((res: Response) => {
              return super.serviceErrorNavigate(res, this.router);
            }));
  }

  SalvarDadosPessoais(dados: UsuarioDadosPessoais): Observable<any> {
    return this.http
    .post(this.UrlService + '/Conta/SalvarDadosPessoais', dados, this.ObterHeaderJson())
    .pipe(catchError((res: Response) => {
      return super.serviceErrorNavigate(res, this.router);
    }));
  }

  MinhasSolicitacoes(id: number): Observable<MinhasSolicitacoesDataGrid[]>{
    return this.http
      .get<MinhasSolicitacoesDataGrid[]>(this.UrlService + '/Solicitacoes/MinhasSolicitacoes/' + id, this.ObterAuthHeaderJson())
      .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }
  ConsultarBoleto(id: number): Observable<any>{
    return this.http
      .get<any>(this.UrlService + '/Solicitacoes/ConsultarBoleto/' + id, this.ObterHeaderText())
      .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }

  ObterTermoUso(descricao: string): Observable<TermoUso>{
    return this.http
      .post<TermoUso>(this.UrlService + '/Carrinho/ObterTermoConcordancia?descricao=' + descricao, super.ObterAuthHeaderJson())
      .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }
}
