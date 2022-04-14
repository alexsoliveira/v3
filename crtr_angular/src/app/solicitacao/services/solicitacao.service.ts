import { SolicitacaoExistente } from './../nova-solicitacao/cadastro-procuracoes/models/solicitacao-existente.model';
import { StatusSolicitacao } from './../status-solicitacao/models/status-solicitacao.model';
import { SolicitacaoAtoCartorial } from '../models/solicitacaoAtoCartorial.model';
import { Solicitacao } from '../models/solicitacao.model';
import { HttpClient } from '@angular/common/http';
import { SolicitacaoSimplificado } from '../models/solicitacao-simplificado.model';
import { Injectable } from '@angular/core';
import { BaseService } from 'src/app/services/base.service';
import { BehaviorSubject, Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { SolicitacaoPartesDataGrid } from '../models/solicitacaoPartesDataGrid.model';
import { SolicitacaoParte } from '../models/solicitacaoParte.model';
import { SolicitacoesAtosCartoriais } from '../models/solicitacoesAtosCartoriais.model';
import { Router } from '@angular/router';
import { ProcuracoesPartesEstadosPc } from '../status-solicitacao/models/procuracoes-partes-estados.model';
import { StatusSolicitacaoHeader } from '../status-solicitacao/models/status-solicitacao-header.model';
import { Solicitante } from '../pagamento/models/Solicitante.model';
import { SolicitacoesEstados } from '../status-solicitacao/models/solicitacoes-estados.model'

@Injectable({
  providedIn: 'root'
})
export class SolicitacaoService extends BaseService {

  private bsParticipantesCompPreco = new BehaviorSubject<any>(undefined);
  private bsParticipanteCompPag = new BehaviorSubject<any>(undefined);
  private bsParticipantesDoc = new BehaviorSubject<any>(undefined);
  private bsParticipantesOpcaoDoc = new BehaviorSubject<any>(undefined);

  constructor(private http: HttpClient, public router: Router) { super(router); }

  // Adicionar uma nova solicitação
  AdicionarNovaSolicitacao(solicitacao: SolicitacaoSimplificado): Observable<number> {
    console.log(JSON.stringify(solicitacao));
    console.log(super.ObterAuthHeaderJson);
    return this.http
      .post(this.UrlService + '/Solicitacoes/IncluirSolicitacao', solicitacao, super.ObterAuthHeaderJson())
      .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }

  atualizarStatusSolicitacao(idSolicitacao: number): any {
    return this.http
      .post(this.UrlService + `/Solicitacoes/AtualizarSolicitacaoParaAguardandoAssinaturaDigitalSolicitante/${idSolicitacao}`, super.ObterAuthHeaderJson())
      .toPromise();
  }

  consultarStatusSolicitacao(idSolicitacao: number): any {
    return this.http
      .get(this.UrlService + `/Solicitacoes/ConsultarStatusSolicitacao/${idSolicitacao}`, super.ObterAuthHeaderJson())
      .toPromise();
  }

  buscarDadosProcuracaoSolicitante(idSolicitacao: number): any {
    return this.http
      .get(this.UrlService + `/Solicitacoes/BuscarDadosProcuracaoSolicitante/${idSolicitacao}`, super.ObterAuthHeaderJson())
      .toPromise();
  }

  AtualizarNovaSolicitacao(solicitacao: Solicitacao): Observable<any> {
    return this.http
      .post(this.UrlService + '/Solicitacoes/IncluirSolicitacaoParte', solicitacao, super.ObterAuthHeaderJson())
      .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }

  AtualizarParte(solicitacao: Solicitacao): Observable<any> {
    return this.http
      .post(this.UrlService + '/SolicitacoesPartes/AtualizarSolicitacaoParte', solicitacao, super.ObterAuthHeaderJson())
      .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }

  ObterTodasAsPartesPorIdSolicitacao(idSolicitacao: number): Observable<SolicitacaoPartesDataGrid[]> {
    // console.log('ObterTodasAsPartesPorIdSolicitacao');
    // console.log(idSolicitacao);
    return this.http
      .get(this.UrlService + '/SolicitacoesPartes/BuscarIdSolicitacao/' + idSolicitacao, super.ObterAuthHeaderJson())
      .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }

  ObterPartePorId(idParte: number): Observable<SolicitacaoParte> {
    return this.http
      .get(this.UrlService + '/SolicitacoesPartes/BuscarId/' + idParte, super.ObterAuthHeaderJson())
      .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }

  DeletarPartePorId(idParte: number): Observable<SolicitacaoParte> {
    console.log(idParte);
    console.log(super.ObterAuthHeaderJson);
    return this.http
      .delete(this.UrlService + '/SolicitacoesPartes/DeletarId/' + idParte, super.ObterAuthHeaderJson())
      .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }

  IncluirSolicitacaoAtoCartorial(solicitacoesAtosCartoriais: SolicitacoesAtosCartoriais): Observable<any> {
    return this.http
      .post(`${this.UrlService}/SolicitacoesAtosCartoriais/IncluirSolicitacaoAtoCartorial`,
        solicitacoesAtosCartoriais, super.ObterAuthHeaderJson())
      .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }

  DadosSolicitacao(idSolicitacao: number): Observable<any> {
    return this.http
      .get(this.UrlService + '/Solicitacoes/BuscarId/' + idSolicitacao, super.ObterAuthHeaderJson())
      .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }


  BuscarCustos(cep: any): Observable<any> {
    return this.http
      .get(this.UrlService + '/TiposFretesPC/BuscarCustos/' + cep, super.ObterAuthHeaderJson())
      .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }

  // Pagamento
  Pagamento(obj: any): boolean {


    // Cartão de crédito
    if (obj.tipo === 1) {

      return true;

    }
    // boleto
    if (obj.tipo === 2) {

      return true;

    }
  }

  obterValorFrete(): BehaviorSubject<any> {
    return this.bsParticipanteCompPag;
  }

  ValorFrete(valor: any): void {
    this.bsParticipanteCompPag.next(valor);
  }

  EnviarObjetoComposicaoPreco(obj: any): void {
    this.bsParticipantesCompPreco.next(obj);
  }
  ObterObjetoComposicaoPreco(): BehaviorSubject<any> {
    return this.bsParticipantesCompPreco;
  }

  EnviarObjetoParticipanteDoc(obj: any): void {
    this.bsParticipantesDoc.next(obj);
  }
  ObterObjetoParticipanteDoc(): BehaviorSubject<any> {
    return this.bsParticipantesDoc;
  }

  EnviarObjetoParticipanteOpcaoDoc(obj: any): void {
    this.bsParticipantesOpcaoDoc.next(obj);
  }
  ObterObjetoParticipanteOpcaoDoc(): BehaviorSubject<any> {
    return this.bsParticipantesOpcaoDoc;
  }

  StatusSolicitacaoPorParticipantes(idSolicitacao: number): Observable<StatusSolicitacao[]> {
    return this.http
      .get<StatusSolicitacao[]>(this.UrlService + '/Solicitacoes/BuscarEstadoDoPedidoPorParticipante/' + idSolicitacao, super.ObterAuthHeaderJson())
      .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }

  ProcuracoesPartesEstadosAtivos(): Observable<ProcuracoesPartesEstadosPc[]> {
    return this.http
      .get<ProcuracoesPartesEstadosPc[]>(this.UrlService + '/Solicitacoes/BuscarTodasProcuracoesPartesEstadosQueEstaoAtivos/', super.ObterAuthHeaderJson())
      .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }

  buscarSolicitacoesEstados(idSolicitacao: number): Observable<SolicitacoesEstados[]> {
    return this.http
      .get<SolicitacoesEstados[]>(this.UrlService + `/Solicitacoes/BuscarSolicitacoesEstados/${idSolicitacao}`, super.ObterAuthHeaderJson())
      .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }

  BuscarDadosSolicitacaoHeader(idSolicitacao: number): Observable<StatusSolicitacaoHeader> {
    return this.http
      .get<StatusSolicitacaoHeader>(this.UrlService + `/Solicitacoes/BuscarDadosStatusSolicitacao/${idSolicitacao}`, super.ObterAuthHeaderJson())
      .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }

  BuscarSolicitante(idSolicitacao: number): Observable<Solicitante> {
    return this.http
      .get<Solicitante>(this.UrlService + '/Carrinho/BuscarSolicitante/' + idSolicitacao, super.ObterAuthHeaderJson())
      .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }

  ObterSolicitacaoExistente(id: number): Observable<SolicitacaoExistente> {
    return this.http
      .get<SolicitacaoExistente>(this.UrlService + `/Solicitacoes/BuscarSolicitacao/${id}`, super.ObterAuthHeaderJson())
      .pipe(catchError((res: Response) => {
        return super.serviceErrorNavigate(res, this.router);
      }));
  }

}
