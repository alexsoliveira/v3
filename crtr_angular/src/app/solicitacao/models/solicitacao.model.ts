import { SolicitacaoDocumento } from "./solicitacaoDocumento.model";

export class Solicitacao {
  idSolicitacao?: number;
  idSolicitacaoParte?: number;
  idProduto: number;
  idUsuario: number;
  idTipoDocumento: number;
  numeroDocumento: number;
  idGenero: number;
  idTipoParte: number;
  email: string;
  nomePessoa?: string;
  //nomeSocial?: string;
  nomeFantasia?: string;
  razaoSocial?: string;
  solicitacoesDocumentos: SolicitacaoDocumento;
  
  // uploadRG?: string;
  // uploadContrato?: string;
}
