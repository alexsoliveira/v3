import { Component, Input, OnInit } from '@angular/core';

import { SolicitacaoService } from '../../services/solicitacao.service';

import { StatusSolicitacao } from './../models/status-solicitacao.model';
import { ProcuracoesPartesEstadosPc } from './../models/procuracoes-partes-estados.model';
import { ItemTrack } from '../../status-solicitacao/tracking/models/item-track.model';


@Component({
  selector: 'app-tracking',
  templateUrl: './tracking.component.html',
  styleUrls: ['./tracking.component.scss']
})
export class TrackingComponent implements OnInit {

  @Input() statusSolicitacao: StatusSolicitacao;
  listItemTrack = new Array();
  procuracoesPartesEstadosPc: ProcuracoesPartesEstadosPc[] = [];

  constructor(
    private solicitacaoService: SolicitacaoService
  ) { }

  ngOnInit(): void {
    this.buscarTracking();
  }

  buscarTracking() {
    this.solicitacaoService.ProcuracoesPartesEstadosAtivos()
      .subscribe((procuracoesPartesEstadosPc) => {
        this.procuracoesPartesEstadosPc = procuracoesPartesEstadosPc;
        this.preencherTracking();
      });
  }

  preencherTracking(): void {

    this.procuracoesPartesEstadosPc.forEach(element => {
      let track = new ItemTrack();
      track.idTrack = element.idProcuracaoParteEstado;
      track.descricao = element.descricao;
      track.status = '';
      this.listItemTrack.push(track);
    });

    this.listItemTrack.forEach(element => {
      let objectJSON = JSON.stringify(this.statusSolicitacao);
      localStorage.setItem("_s",objectJSON);
      let objectString = localStorage.getItem("_s");
      let obj = JSON.parse(objectString);
      if (element.idTrack <= obj.idEstadoParticipante){
        element.status = 'active';
      }
    })

  }
}
