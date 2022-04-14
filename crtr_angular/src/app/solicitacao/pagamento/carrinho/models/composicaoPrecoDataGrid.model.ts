export class ComposicaoPrecoDataGrid {

  constructor(
    disponivel: boolean,
    servico: string,
    observacao: string,
    destino:string,
    custo:any)
  {
    this.disponivel = disponivel;
    this.servico = servico;
    this.observacao = observacao;
    this.destino = destino;
    this.custo = custo;
  }

  disponivel: boolean;
  servico: string;
  observacao: string;
  destino:string;
  custo:any;
}
