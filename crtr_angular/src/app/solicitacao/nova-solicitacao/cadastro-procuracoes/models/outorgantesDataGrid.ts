export class OutorgantesDataGrid {

  constructor(idSolicitacaoParte: number,
              nomePessoa: string,
              documento: string,
              email: string
  ) {
    this.idSolicitacaoParte = idSolicitacaoParte;
    this.nomePessoa = nomePessoa;
    this.documento = documento;
    this.email = email;
  }

  idSolicitacaoParte: number;
  nomePessoa: string;
  documento: string;
  email: string;
}
