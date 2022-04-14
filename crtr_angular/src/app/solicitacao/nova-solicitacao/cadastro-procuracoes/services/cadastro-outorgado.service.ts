
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { BaseService } from 'src/app/services/base.service';
import { Router } from '@angular/router';
import { SolicitacaoOutorgados } from '../models/solicitacao-outorgados.model';
import { UsuarioSolicitante } from 'src/app/solicitacao/nova-solicitacao/cadastro-procuracoes/components/cadastro-outorgado/models/usuario-solicitante.model';

@Injectable({
  providedIn: 'root'
})
export class CadastroOutorgadoService extends BaseService {

  constructor(private http: HttpClient, public router: Router) { super(router); }

  AdicionarOutorgados(solicitacaoOutorgados: SolicitacaoOutorgados): Observable<any> {
    console.log(super.ObterAuthHeaderJson);
    return this.http
      .post(this.UrlService + '/Solicitacoes/CriarOutorgados', solicitacaoOutorgados, super.ObterAuthHeaderJson())
      .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }

  BuscarDadosUsuario(id: number): Observable<UsuarioSolicitante> {
    return this.http
           .get<UsuarioSolicitante>(this.UrlService + '/Conta/BuscarDadosUsuario/' + id, this.ObterHeaderJson())
           .pipe(catchError((res: Response) => {
              return super.serviceErrorNavigate(res, this.router);
            }));
  }
}
