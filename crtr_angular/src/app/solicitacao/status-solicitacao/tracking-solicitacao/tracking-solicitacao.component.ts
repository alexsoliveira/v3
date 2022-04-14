import { Component, Input, OnInit } from '@angular/core';

import { SolicitacaoService } from '../../services/solicitacao.service';

import { SolicitacoesEstados } from './../models/solicitacoes-estados.model';
import { SolicitacoesEstadosPc } from './../models/solicitacoes-estados-pc.model';
import { ItemTrack } from '../../status-solicitacao/tracking/models/item-track.model'
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-tracking-solicitacao',
  templateUrl: './tracking-solicitacao.component.html',
  styleUrls: ['./tracking-solicitacao.component.scss']
})
export class TrackingSolicitacaoComponent implements OnInit {

  idSolicitacao: number = undefined;
  solicitacoesEstados: SolicitacoesEstados[] = [];
  listItemTrack = new Array();
  solicitacoesEstadosPc: SolicitacoesEstadosPc[] = [];

  constructor(
    private solicitacaoService: SolicitacaoService,
    private activatedRoute: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.idSolicitacao = this.activatedRoute.snapshot.params.idSolicitacao;
    this.solicitacoesEstadosPc = SolicitacoesEstadosPc.getListTracking();
    this.buscarSolicitacoesEstados();
  }

  buscarSolicitacoesEstados() {
    this.solicitacaoService.buscarSolicitacoesEstados(this.idSolicitacao)
      .subscribe((solicitacoesEstados) => {
        if (solicitacoesEstados && solicitacoesEstados.length > 0) {
          solicitacoesEstados.forEach(estado => this.solicitacoesEstados.push({
            idSolicitacao: estado.idSolicitacao,
            idEstado: estado.idEstado,
            dataOperacao: estado.dataOperacao
          }));

          this.preencherTracking();
        }
      },
      (err) => {
        console.log(err);
      });
  }

  preencherTracking(): void {

    this.solicitacoesEstadosPc.forEach(element => {
      let track = new ItemTrack();
      track.idTrack = element.idSolicitacoesEstadosPc;
      track.descricao = element.descricao;
      track.status = '';
      this.listItemTrack.push(track);
    });

    this.listItemTrack.forEach(element => {
      if (this.solicitacoesEstados && this.solicitacoesEstados.length > 0) {
        if(this.solicitacoesEstados.find(se => se.idEstado === element.idTrack)){
          element.status = 'active';
        }
      }
    })

  }
}
