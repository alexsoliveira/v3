import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTable } from '@angular/material/table';
import { utilsBr } from 'js-brasil';
import { BehaviorSubject } from 'rxjs';

import { ModalComponent } from 'src/app/shared/modal/modal.component';
import { OutorgadosDataGrid } from './../../models/outorgadosDataGrid';


@Component({
  selector: 'app-datagrid-outorgado',
  templateUrl: './datagrid-outorgado.component.html',
  styleUrls: ['./datagrid-outorgado.component.scss']
})
export class DatagridOutorgadoComponent implements OnInit {

  @Input() dataSourceBehaviorSubject: BehaviorSubject<OutorgadosDataGrid[]>;
  @Input() IdNovaSolicitacao: number;
  @Input() dataSource: OutorgadosDataGrid[] = [];
  @Output() EventoEditarParte = new EventEmitter();
  @Output() EventoDeletarParte = new EventEmitter();

  MASKS = utilsBr.MASKS;
  IdParte: number;
  displayedColumns: string[] = ['nomePessoa', 'documento', 'acoes'];

  constructor(
    public dialog: MatDialog
  ) { }

  @ViewChild(MatTable) table: MatTable<OutorgadosDataGrid>;

  ngOnInit(): void {

  }

  isCPF(valor: number, tipo: string): boolean {
    if (tipo.toUpperCase() === 'CPF') {
      return true;
    }
    return false;
  }

  editarParte(id: number): void {
    this.IdParte = id;
    this.EventoEditarParte.emit(this.IdParte);
  }

  abrirModal(linha): void {
    const dialogRef = this.dialog.open(ModalComponent);

    dialogRef.componentInstance.titulo = "Excluir participante";
    dialogRef.componentInstance.conteudo = "Deseja realmente excluir o participante?";
    dialogRef.componentInstance.nomeIconeCancelar = "clear";
    dialogRef.componentInstance.nomeBotaoCancelar = "Cancelar";
    dialogRef.componentInstance.nomeIconeOk = "delete";
    dialogRef.componentInstance.nomeBotaoOk = "Excluir Participante";

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);

      if (result === true)
      {
        const index: number = this.dataSource.indexOf(linha);
        if(index !== -1){
          this.dataSource.splice(index,1);
          console.log('abrirModal: ');
          console.log(this.dataSource);
          this.table.renderRows();
          this.EventoDeletarParte.emit(linha);
        }
      }
    });

  }

  deletarParte(id: number): void {
    this.IdParte = id;
    this.EventoDeletarParte.emit(this.IdParte);
  }

  formatarCPF(valor: number): string {
    return ('00000000000' + valor).slice(-11);
  }

  formatarCNPJ(valor: number): string {
    return ('00000000000000' + valor).slice(-14);
  }

  updateGridRows():void {
    console.log('updateGridRows: ');
    console.log(this.dataSource);
    this.table.renderRows();
  }
}
