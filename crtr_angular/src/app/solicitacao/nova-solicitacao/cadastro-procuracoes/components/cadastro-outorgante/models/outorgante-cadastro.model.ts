import { NovoEndereco } from "src/app/shared/models/NovoEndereco.model";

export class OutorganteCadastro{
  idPessoa: number;
  nomePessoa: string;
  nascimento: string;
  idTipoDocumento: number;
  documento: string;
  rg: string;
  profissao: string;
  estadoCivil: string;
  nacionalidade: string;
  email: string;
  // ordemAprovacaoMinuta: string;
  celular: string;
  foneAlternativo: string;
  endereco: NovoEndereco;
}
