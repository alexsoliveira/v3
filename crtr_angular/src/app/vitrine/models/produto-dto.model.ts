import { Injectable } from '@angular/core';
import { Modalidade } from 'src/app/shared/models/Modalidade.model';

@Injectable({
  providedIn: 'root'
})
export class ProdutoDTO {
  idProduto: number;
  titulo: string;
  descricao: string;
  blobImagemProduto: string;
  modalidades: Modalidade[];
  // blobModalidade: string;
  // tituloModalidade: string;
  // descricaoModalidade: string;
}
