import { Endereco } from '../../shared/models/Endereco.model';
import { Contato } from '../models/contato.model';
export class UsuarioDadosPessoais {
  idPessoa: number;
  idUsuario: number;
  nomeSocial: string;
  nascimento: string;
  estadoCivil: number;
  profissao: string;
  nacionalidade: string;
  contato: Contato;
}