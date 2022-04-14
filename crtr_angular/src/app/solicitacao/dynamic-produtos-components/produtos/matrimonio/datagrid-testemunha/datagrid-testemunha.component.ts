import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { utilsBr } from 'js-brasil';
import { MatTable } from '@angular/material/table';

import { TestemunhasDataGrid } from '../models/testemunhasDatagrid';
import { ModalComponent } from 'src/app/shared/modal/modal.component';

// const ELEMENT_DATA: TestemunhasDataGrid[] = [
//   { idSolicitacaoParte: 1, nomePessoa: 'João da Silva', rgTestemunha: '12345678-9', parteTestemunha: 1 },
//   { idSolicitacaoParte: 2, nomePessoa: 'José Alencar', rgTestemunha: '12345678-9', parteTestemunha: 1 },
//   { idSolicitacaoParte: 3, nomePessoa: 'Maria Andrade', rgTestemunha: '12345678-9', parteTestemunha: 2 },
//   { idSolicitacaoParte: 4, nomePessoa: 'Ana Clara', rgTestemunha: '12345678-9', parteTestemunha: 2 }
// ];

@Component({
  selector: 'app-datagrid-testemunha',
  templateUrl: './datagrid-testemunha.component.html',
  styleUrls: ['./datagrid-testemunha.component.scss']
})
export class DatagridTestemunhaComponent implements OnInit {

  @Input() IdNovaSolicitacao: number;
  @Input() dataSource: TestemunhasDataGrid[] = []
  @Output() EventoEditarParte = new EventEmitter();
  @Output() EventoDeletarParte = new EventEmitter();
  @Output() dadosTestemunhas = new EventEmitter();

  MASKS = utilsBr.MASKS;
  IdParte: number;
  displayedColumns: string[] = ['nomePessoa', 'documento', 'acoes'];
  @ViewChild(MatTable) table: MatTable<TestemunhasDataGrid>;

  constructor(
    public dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.consultarPartes(this.IdNovaSolicitacao);
  }

  consultarPartes(idSolicitacao: number): void {
    // console.log(idSolicitacao);
    if (Number(idSolicitacao) !== 0 && String(idSolicitacao) !== 'undefined') {
      // this.solicitacaoService.ObterTodasAsPartesPorIdSolicitacao(idSolicitacao)
      // .subscribe(
      //   datagrid => {
      //     this.dataSource = datagrid;
      //     console.log(this.dataSource);
      //     console.log(idSolicitacao);
      //     this.IdNovaSolicitacao = idSolicitacao;
      //   },
      //   error => console.log(error)
      // );
    }
    else {
      // console.log('passou consultarPartes');
    }
  }

  isCPF(valor: number, tipo: string): boolean {
    if (tipo.toUpperCase() === 'CPF') {
      return true;
    }
    // return (valor.toString().length === 11);
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
      if (result === true) {
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

  // getTipoProduto(tipoProduto) {
  //   switch(tipoProduto) {
  //     case 'Contrair Matrimonio':

  //     case 'Bens Imo':

  //   }
  // }

}
