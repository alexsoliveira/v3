import { ProdutosModalidadeNavigation } from './produtoModalidadeNavigation.model';
import { ModalidadesConteudo } from './modalidadesConteudo.model';

export class ProdutosModalidades{
  idProdutoModalidade: number;
  idProduto: number;
  idModalidade: number;
  conteudo: ModalidadesConteudo;
  conteudoObj: ModalidadesConteudo;
  idModalidadeNavigation: ProdutosModalidadeNavigation;
}

// export class ProdutosModalidades{
//   idProdutoModalidade: number;
//   idProduto: number;
//   idModalidade: number;
//   // dataOperacao;
//   // idUsuario;
//   // dataInicio;
//   // dataFim;
//   conteudo;
// }

// export class Produtos{
//   idProduto: number;
//   titulo: string;
//   descricao: string;
// //   dataOperacao;
// //   idUsuario: number;
// //   campos: string;
// //   dataInicio
// //   dataFim
//   flagAtivo: Boolean;
//   subTitulo: string;
// }
