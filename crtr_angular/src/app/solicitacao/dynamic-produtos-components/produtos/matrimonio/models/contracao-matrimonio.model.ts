import { TestemunhaCadastro } from "../cadastro-testemunha/models/testemunha-cadastro.model";
import { TestemunhasDataGrid } from "./testemunhasDatagrid";

export class ContracaoMatrimonioCadastro{
  UploadArquivo: string;
  NomeArquivoProclamas: string;
  RegimeCasamento: string;

  NomeRequerente: string;
  TipoDocumentoRequerente: string;
  DocumentoRequerente: string;
  DataNascimentoRequerente: string;

  NomeMaeRequerente: string;
  TipoDocumentoMaeRequerente: string;
  DocumentoMaeRequerente: string;
  DataNascimentoMaeRequerente: string;
  SituacaoMaeRequerente: string;

  NomePaiRequerente: string;
  TipoDocumentoPaiRequerente: string;
  DocumentoPaiRequerente: string;
  DataNascimentoPaiRequerente: string;
  SituacaoPaiRequerente: string;

  NomeNoivos: string;
  TipoDocumentoNoivos: string;
  DocumentoNoivos: string;
  DataNascimentoNoivos: string;

  NomeMaeNoivos: string;
  TipoDocumentoMaeNoivos: string;
  DocumentoMaeNoivos: string;
  DataNascimentoMaeNoivos: string;
  SituacaoMaeNoivos: string;

  NomePaiNoivos: string;
  TipoDocumentoPaiNoivos: string;
  DocumentoPaiNoivos: string;
  DataNascimentoPaiNoivos: string;
  SituacaoPaiNoivos: string;

  Testemunhas: TestemunhaCadastro[] = [];

  formularioValido: boolean = false;
  testemunhaRequerenteValido: boolean = false;
  testemunhaNoivoOuNoivaValido: boolean = false;
}
