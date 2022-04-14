import { ProdutosImagens } from './produtosImagens.model';
import { Produtos } from './produtos.model';

export class CategoriaProduto{
  idProdutoCategoria: number;
  titulo: string;
  descricao: string;
  produtos: Produtos[];

  static produtosConfiguration(categoriaProdutos: any): CategoriaProduto[]{
    if(categoriaProdutos && categoriaProdutos.length > 0){
      categoriaProdutos.forEach(cp => {
        cp.produtos.forEach(p => {
          if(p.campos){
            p.produtoCampos = JSON.parse(p.campos);
            if(p.produtoCampos.disponivel != undefined){
              p.isVisible = p.produtoCampos.disponivel;
            }
          }
        })
      })
    }

    return categoriaProdutos;
  }
}
