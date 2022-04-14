import { ProdutosImagens } from './produtosImagens.model';
import { ProdutosModalidades } from './produtosModalidades.model';
import { ProdutoCampos } from './produto-campos.model';

export class Produtos{
  idProduto: number;
  titulo: string;
  descricao: string;
  dataOperacao: string;
  campos: string;
  produtoCampos: ProdutoCampos;
  produtosImagens: ProdutosImagens[] = [];
  produtosModalidades: ProdutosModalidades[];
  isVisible: boolean;

  getCampos(): boolean {
    if(this.campos){
      this.produtoCampos = JSON.parse(this.campos);
      return true;
    }
    return false;
  }
}


