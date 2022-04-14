import { ProdutosModalidades } from "src/app/vitrine/models/produtosModalidades.model";

export class Modalidade{
  idModalidade: number;
  descricao: string;
  titulo: string;
  blobConteudo: string;
  strBlobConteudo: string;

  static getPreecherModalidade(produtosModalidades: ProdutosModalidades[], modalidades: Modalidade[]): Modalidade[] {
    let modalidadesProduto: Modalidade[] = [];

    if (produtosModalidades){
      produtosModalidades.forEach(produtoModalidade => {
        let modalidade = modalidades.find(m => m.idModalidade == produtoModalidade.idModalidade);
        if (modalidade) {
          modalidadesProduto.push(modalidade);
        }
      })
    }
    return modalidadesProduto;
  }
}
