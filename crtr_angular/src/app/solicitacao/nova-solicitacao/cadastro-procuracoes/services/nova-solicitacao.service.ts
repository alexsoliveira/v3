
import { HttpClient, HttpEvent } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Usuario } from 'src/app/conta/models/usuario.model';
import { BaseService } from 'src/app/services/base.service';
import { BoletoBancarioComponent } from 'src/app/solicitacao/player-pagamento/boleto-bancario/boleto-bancario.component';
import { InformacoesImportantes } from '../models/informacoes-importantes.model';

@Injectable({
  providedIn: 'root'
})

export class NovaSolicitacaoService extends BaseService {

  constructor(private http: HttpClient, public router: Router) { super(router); }

  

  adicionarInformacoesImportantes(info: InformacoesImportantes): Observable<any> {
    console.log(super.ObterAuthHeaderJson);
    return this.http
      .post(this.UrlService + `/Solicitacoes/CadastrarInfomacoesImportantes`, 
              info, super.ObterAuthHeaderJson())
      .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }


  validarProduto(produto: string, modalidade: string): string {
    let descricao = '';
    switch (produto) {
      case 'matrimonio':
        descricao = 'matrimonio';
        break;
      default:
        return null;
    }

    // switch (modalidade) {
    //   case 'online':
    //     descricao = `${descricao} Online`;
    //     break;
    //   default:
    //     return null;
    // }

    return descricao;
  }

  emailPossuiConta(email: String): Observable<Boolean> {
    return this.http
      .get<Boolean>(this.UrlService + `/Conta/EmailPossuiConta?email=${email}`);
  }

  emailPerteceAoDocumento(documento: number, email: string): Observable<Boolean> {
    return this.http
      .get<Boolean>(this.UrlService + `/Conta/EmailPertenceAoDocumento/${documento}?email=${email}`);
  }


  getIdProdutoPeloNome(produto: string): number {
    if (!produto) {
      return null;
    }

    switch (produto) {

      case 'matrimonio':
        return 1;

      case 'compra-venda-imoveis':
        return 2;

      default:
        return null;
    }
  }

  VerificarDadosSolicitanteAtualizados(id: number): Observable<boolean> {
    return this.http
           .get<boolean>(this.UrlService + '/Conta/VerificarDadosSolicitanteAtualizados/' + id, this.ObterHeaderJson())
           .pipe(catchError((res: Response) => {
              return super.serviceErrorNavigate(res, this.router);
            }));
  }
}
