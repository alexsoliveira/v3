
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { BaseService } from 'src/app/services/base.service';
import { Outorgante } from './../models/outorgante.model';
import { Router } from '@angular/router';
import { Usuario } from 'src/app/conta/models/usuario.model';
import { UsuarioSolicitanteDTO } from '../../models/usuario-solicitante-dto.model';
import { SolicitacaoOutorgantes } from '../models/solicitacao-outorgantes.model';


@Injectable({
  providedIn: 'root'
})
export class CadastroOutorganteService extends BaseService {

  constructor(private http: HttpClient, public router: Router) { super(router); }

  BuscarDadosSolicitante(idTipoDocumento: number, documento: string, idUsuario: number): Observable<UsuarioSolicitanteDTO> {
    return this.http
      .get(this.UrlService + `/Conta/BuscarDadosUsuarioSolicitante/${idUsuario}/${idTipoDocumento}/${documento}`, 
      super.ObterAuthHeaderJson())
      .pipe(catchError((res: Response)=>{
        return super.serviceErrorNavigate(res, this.router);
      }));
  }

  AdicionarOutorgantes(outorgantes: SolicitacaoOutorgantes): Observable<any> {
    console.log(JSON.stringify(outorgantes));
    console.log(super.ObterAuthHeaderJson);
    return this.http
      .post(this.UrlService + '/Solicitacoes/CriarOutorgantes', outorgantes, super.ObterAuthHeaderJson())
      .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }

  PessoaExiste(idTipoDocumento: number, documento: string){
    return this.http
    .get(this.UrlService + `/Pessoas/PessoaExiste/${idTipoDocumento}/${documento}`, super.ObterAuthHeaderJson())
    .pipe(catchError((res: Response)=>{
      return super.serviceErrorNavigate(res, this.router);
    }));
  }
  
}
