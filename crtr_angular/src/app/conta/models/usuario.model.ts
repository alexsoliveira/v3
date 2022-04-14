import { NovoEndereco } from 'src/app/shared/models/NovoEndereco.model';
import { Endereco } from '../../shared/models/Endereco.model';
import { Contato } from '../models/contato.model';
export class Usuario {
  nome: string;
  nomeSocial: string;
  email: string;
  idTipoDocumento: number;
  documento: string;
  nascimento: string;
  estadoCivil: number;
  nacionalidade: string;
  profissao: string;
  rg: string;
  contatos: Contato[] = [];
  enderecos: NovoEndereco[] = [];
}