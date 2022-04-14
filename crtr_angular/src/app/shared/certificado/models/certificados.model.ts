export class Certificados {
  IdMatrimonio: number;
  IdPessoaSolicitante: number;
  IdUsuario: number;
  DescricaoCertificado: string;
  DocumentoCertificado: [];
  DocumentoPDF: string;
  Emissor: string;
  ModeloCertificado: string;
  NumeroSerie: string;
  Senha: string;
  Sujeito: string;
  ValidadeCertificado: string;
  Valor: string;
  ThumbPrintBase64: string;
  CertificadoBase64: string;

  constructor(emissor: string, sujeito:string, validade: string, modeloCertificado: string, thumbPrint: string) {
    this.Emissor = emissor;
    this.Sujeito = sujeito;
    this.ValidadeCertificado = validade;
    this.ModeloCertificado = modeloCertificado;
    this.ThumbPrintBase64 = thumbPrint;
  }
}
