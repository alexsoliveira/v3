export class SolicitacaoSimplificado {
  idSolicitacao: number;
  idProduto: number;
  idUsuario: number;
  idPessoaSolicitante: number;

  constructor (idProduto: number,
               idUsuario: number,
               idPessoaSolicitante: number) {
    this.idProduto = idProduto;
    this.idPessoaSolicitante = idPessoaSolicitante;
    this.idUsuario = idUsuario;
  }
}
