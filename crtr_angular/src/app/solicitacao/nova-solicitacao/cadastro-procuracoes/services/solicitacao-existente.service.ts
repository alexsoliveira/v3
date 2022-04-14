import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { OutorgadosDataGrid } from '../models/outorgadosDataGrid';
import { OutorgantesDataGrid } from '../models/outorgantesDataGrid';

@Injectable({
  providedIn: 'root'
})
export class SolicitacaoExistenteService {

  private bsSolicitacaoExistenteOutorgante: BehaviorSubject<OutorgantesDataGrid[]>;
  private bsSolicitacaoExistenteOutorgado: BehaviorSubject<OutorgadosDataGrid[]>;

  constructor() { }

  setSolicitacaoExistenteOutorgante(solicitacao: OutorgantesDataGrid[]): void {
    console.log(solicitacao);
    this.bsSolicitacaoExistenteOutorgante = new BehaviorSubject(solicitacao);
    this.bsSolicitacaoExistenteOutorgante.subscribe((obj) => {
      
    });
  }

  getSolicitacaoExistenteOutorgante(): Observable<OutorgantesDataGrid[]> {
    return this.bsSolicitacaoExistenteOutorgante.asObservable();
  }

  setSolicitacaoExistenteOutorgado(solicitacao: OutorgadosDataGrid[]): void {
    console.log(solicitacao);
    this.bsSolicitacaoExistenteOutorgado = new BehaviorSubject(solicitacao);
    this.bsSolicitacaoExistenteOutorgado.subscribe((obj) => {

    });
  }

  getSolicitacaoExistenteOutorgado(): Observable<OutorgadosDataGrid[]> {
    return this.bsSolicitacaoExistenteOutorgado.asObservable();
  }

}
