export class TestemunhasDataGrid {

  constructor(
    idSolicitacaoParte: number,
    nomePessoa: string,
    idTipoDocumento: string,
    documento: string,
    rgTestemunha: string,
    parteTestemunha: string,
  ) {
    this.idSolicitacaoParte = idSolicitacaoParte;
    this.nomePessoa = nomePessoa;
    this.idTipoDocumento = idTipoDocumento;
    this.documento = documento;
    this.rgTestemunha = rgTestemunha;
    this.parteTestemunha = parteTestemunha;
  }

  idSolicitacaoParte: number;
  nomePessoa: string;
  idTipoDocumento: string;
  documento: string;
  rgTestemunha: string;
  parteTestemunha: string;
}
