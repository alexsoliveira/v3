import { Injectable } from '@angular/core';
import { Contato } from 'src/app/conta/models/contato.model';
import { NovoEndereco } from 'src/app/shared/models/NovoEndereco.model';

@Injectable({
  providedIn: 'root'
})

export class UsuarioSolicitanteDTO {
  idPessoaSolicitante: number;
  nomeUsuario: string;
  email: string;
  idTipoDocumento: number;
  documento: number;
  dataNascimento: string;
  enderecos: NovoEndereco[] = [];
  contato: Contato;
  rg: string;
  profissao: string;
  nacionalidade: string;
  estadoCivil: string;
}