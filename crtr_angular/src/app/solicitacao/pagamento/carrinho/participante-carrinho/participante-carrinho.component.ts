import { Component, OnInit, Input, AfterViewInit, ViewChildren, QueryList } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { BehaviorSubject } from 'rxjs';
import { ToastOptions } from 'src/app/utils/toastr.config';
import { CepComponent } from '../../../../shared/cep/cep.component';
import { CarrinhoService } from '../services/carrinho.service';

import { Participante } from '../models/participante.model';
import { ParticipanteDataGrid } from '../models/participanteDataGrid.model';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-participante-carrinho',
  templateUrl: './participante-carrinho.component.html',
  styleUrls: ['./participante-carrinho.component.scss']
})
export class ParticipanteCarrinhoComponent implements OnInit, AfterViewInit {

  @Input() dataSource: ParticipanteDataGrid[] = [];
  participante: Participante[] = [];
  idSolicitacao: number;

  bsParticipanteCEP = new BehaviorSubject<any>(undefined);
  @Input() item: any;
  @Input() boOpcaoPai: any;
  @Input() habComp: any;
  @ViewChildren(CepComponent) cepObj: QueryList<CepComponent>;

  displayedColumns: string[] = ['Participante', 'Parte'];
  columnsToDisplay: string[] = this.displayedColumns.slice();
  //datasource: MatTableDataSource<any>;

  //arrParticipantes = new Array();

  constructor(
    private carrinhoService: CarrinhoService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private activatedRoute: ActivatedRoute
  ) { }

  ngAfterViewInit(): void {

  }

  ngOnInit() {
    this.idSolicitacao = this.activatedRoute.snapshot.params.idSolicitacao;

    // TODO: Criação do service Angular chamada dos participante para compor o carrinho
    this.ObterParticipantes(this.idSolicitacao);

    // setTimeout(() => {
    //   console.log(this.habComp);
    // }, 2000);

    // this.plotaGrid();
  }

  ObterParticipantes(idSolicitacao: number): void{
    this.carrinhoService.ObterParticipantes(idSolicitacao)
    .subscribe((ret: Participante[] ) => {
      console.log(ret);
      this.dataSource = ret;
      this.participante = ret;
    },
    (err) => {
      console.log(err);
      try {
        try {
          err.error.errors.Mensagens.forEach(erro => {
            this.toastr.error(`${erro}`, 'Tabelionet', ToastOptions);
          });
        }
        catch {
          Object.keys(err.error.errors).forEach((erro, i, a) => {
            err.error.errors[erro].forEach(msg => {
              this.toastr.error(`${msg}`, 'Tabelionet', ToastOptions);
            });
          });
        }
      }
      catch {
        this.toastr.error(`${err.status} - ${err.message} - ${err.error}`, 'Tabelionet', ToastOptions);
      }
    },
    () => { }
    ).add(() => {
      this.spinner.hide();
    });
  }

  // plotaGrid(): void {

  //   let item = new Participante();

  //   item.Participante = 'João da silva';
  //   item.Parte = 'Outorgante';
  //   item.Impressao = '10.5';
  //   item.Frete = '22.3';
  //   item.Prazo = '3 dias';

  //   this.arrParticipantes.push(item);

  //   item = new Participante();

  //   item.Participante = 'Tereza dos santos';
  //   item.Parte = 'Outorgado';
  //   item.Impressao = '32.5';
  //   item.Frete = '12.8';
  //   item.Prazo = '10 dias';

  //   this.arrParticipantes.push(item);

  //   this.datasource = new MatTableDataSource(this.arrParticipantes);

  // }

  // plotaObjCEP(evt, linha): void {

  //   let obj = this.cepObj.find((e, i) => i === linha);

  //   if (evt){
  //     obj.formgroup.enable();
  //     obj.txtcep.nativeElement.select();
  //   }
  //   else
  //     obj.formgroup.disable();

  //   //console.log(this.datasource);
  //   //console.log(this.cepObj);

  // }

}

// export class Participante {
//   Participante: string;
//   Parte: string;
//   Impressao: string;
//   Frete: string;
//   Prazo: string;
// }
