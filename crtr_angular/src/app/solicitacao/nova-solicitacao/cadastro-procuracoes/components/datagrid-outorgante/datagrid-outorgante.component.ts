import { Input, OnInit, Component, EventEmitter, Output, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTable } from '@angular/material/table';
import { utilsBr } from 'js-brasil';

import { ModalComponent } from 'src/app/shared/modal/modal.component';
import { OutorganteCadastro } from '../cadastro-outorgante/models/outorgante-cadastro.model';
import { OutorgantesDataGrid } from './../../models/outorgantesDataGrid';



@Component({
  selector: 'app-datagrid-outorgante',
  templateUrl: './datagrid-outorgante.component.html',
  styleUrls: ['./datagrid-outorgante.component.scss']
})
export class DatagridOutorganteComponent implements OnInit {

  @Input() IdNovaSolicitacao: number;
  @Input() dataSource: OutorgantesDataGrid[] = [];
  @Output() EventoEditarParte = new EventEmitter();
  @Output() EventoDeletarParte = new EventEmitter();

  MASKS = utilsBr.MASKS;

  IdParte: number;

  // 'receberFisico', 'ordemAprovacaoMinuta',
  displayedColumns: string[] = ['nomePessoa', 'documento', 'acoes'];

  constructor(
    public dialog: MatDialog
  ) { }


  @ViewChild(MatTable) table: MatTable<OutorgantesDataGrid>;

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
        //console.log(this.dataSource.indexOf(id));
        const index: number = this.dataSource.indexOf(linha);
        if(index !== -1){
          this.dataSource.splice(index, 1);
          console.log('abrirModal: ');
          console.log(this.dataSource);
          this.table.renderRows();
          this.EventoDeletarParte.emit(linha);
        }

        // this.dataSource = this.dataSource.filter((value,key)=>{
        //   return value.nomePessoa != id.nomePessoa;
        // });
        // console.log('abrirModal: ');
        // console.log(this.dataSource);
        // this.table.renderRows();
        // this.EventoDeletarParte.emit(id.nomePessoa);
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
    this.table.renderRows();
  }

}
