import { OutorgadoExistente } from "./outorgado-existente.model";
import { OutorganteExistente } from "./outorgante-existente.model";

export class SolicitacaoExistente{
  idSolicitacao: number;
  idPessoaSolicitante: number;
  idProduto: number;
  nomeProduto: string;
  outorgados: OutorgadoExistente[] = [];
  outorgantes: OutorganteExistente[] =[];
  informacoesImportantes: string;
  jsonProduto: string;
  representacaoPartes: string;
}
