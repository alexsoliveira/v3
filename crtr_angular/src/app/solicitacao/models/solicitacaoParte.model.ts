import { SolicitacaoDocumento } from './solicitacaoDocumento.model';

export class SolicitacaoParte{
  IdSolicitacaoParte: number;
  IdTipoParte: number;
  IdTipoDocumento: number;
  NumeroDocumento: number;
  NomePessoa?: string;
  NomeSocial?: string;
  NomeFantasia?: string;
  RazaoSocial?: string;
  Email: string;
  // solicitacoesDocumentos: SolicitacaoDocumento[] = [];
}
