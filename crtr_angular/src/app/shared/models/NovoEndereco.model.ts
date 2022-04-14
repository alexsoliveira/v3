import { EnderecoConteudo } from './EnderecoConteudo.model';

export class NovoEndereco {
  idEndereco: number;
  conteudo: EnderecoConteudo;
  idUsuario: number;
  idPessoa: number;
  flagAtivo: boolean;
  enderecoUsuarioLogado: boolean;
  novoEndereco: boolean;

  setConteudo(enderecoConteudo: any): void {
    this.conteudo = new EnderecoConteudo();
    this.conteudo.uf = enderecoConteudo.uf;
    this.conteudo.bairro = enderecoConteudo.bairro;
    this.conteudo.cep = enderecoConteudo.cep;
    this.conteudo.complemento = enderecoConteudo.complemento;
    this.conteudo.localidade = enderecoConteudo.localidade;
    this.conteudo.logradouro = enderecoConteudo.logradouro;
    this.conteudo.numero = enderecoConteudo.numero;
    this.conteudo = this.conteudo;
  }
}