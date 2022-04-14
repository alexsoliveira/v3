import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';

import { VitrineService } from '../services/vitrine.service';

import { Produto } from './models/produto.model';
import { ProdutoImagem } from './models/produto-imagem.model';
import { Modalidade } from './models/modalidade.model';

import { Produtos } from '../models/produtos.model';
import { ProdutosModalidades } from '../models/produtosModalidades.model';
import { map } from 'rxjs/operators';
import { ProdutoDTO } from '../models/produto-dto.model';
import { LocalStorageUtils } from 'src/app/utils/localstorage';

@Component({
  selector: 'app-detalhamento',
  templateUrl: './detalhamento.component.html',
  styleUrls: ['./detalhamento.component.scss']
})
export class DetalhamentoComponent implements OnInit {
  produto: Produto = new Produto();
  produtoImagem: ProdutoImagem = new ProdutoImagem();
  modalidades: Modalidade[] = [];
  localstorage: LocalStorageUtils = null;
  exibirBotaoSolicitar: Boolean = false;
  setIntervalExibirBotaoSolicitar: any;

  constructor(
    private router: ActivatedRoute,
    private route: Router,
    private vitrineService: VitrineService,
    private spinner: NgxSpinnerService,
    private produtoDTO: ProdutoDTO
  ) { }

  ngOnInit(): void {

    let idProduto = this.router.snapshot.params.id;

    if (this.produtoDTO.idProduto) {
      this.preencherDetalhesProduto(this.produtoDTO);
    
    } else if (idProduto){
      this.detalheProduto(idProduto);

    } else {
        this.redirecionarHome();
    }

    this.localstorage = new LocalStorageUtils();

    if (!this.exibirBotaoSolicitar) {
      this.setIntervalExibirBotaoSolicitar = setInterval(() => {
        if (this.localstorage) {
          this.exibirBotaoSolicitar = this.localstorage.isPoliticaPrivacidadeAcordada();
          if (this.exibirBotaoSolicitar) {
            clearInterval(this.setIntervalExibirBotaoSolicitar);
          }
        }   
      }, 500)
    }
  }

  detalheProduto(id: string): void{
    this.vitrineService.obterProduto(id)
      .subscribe(
        produto => {
          this.preencherDetalhesProduto(produto);
          console.log(this.produto);
        },
        error => {
          this.spinner.hide();
          console.log(error);
          this.redirecionarHome();
        },
        () => { this.spinner.hide(); }
      );
  }


  redirecionarHome() : void {
    this.route.navigate(['/home']);
  }


  preencherDetalhesProduto(produto: any) {
    if (produto) {
      
      if (produto.campos) {
        let campos = JSON.parse(produto.campos);
        if (campos.disponivel !== undefined && !campos.disponivel) {
          this.redirecionarHome();
        }
      }

      this.produto.idProduto = produto.idProduto;
      this.produto.titulo = produto.titulo;
      this.produto.descricao = produto.descricao;
      
      if (produto.blobImagemProduto) {
        this.produtoImagem.blobConteudo = produto.blobImagemProduto;
      } else if (produto.produtosImagens && produto.produtosImagens.length > 0) {
        this.produtoImagem.blobConteudo = produto.produtosImagens[0].strBlobConteudo;
      }

      if (produto.modalidades) {
        this.modalidades = produto.modalidades;
      } else if (produto.produtosModalidades && produto.produtosModalidades.length > 0) {
        this.modalidades = produto.produtosModalidades;
      } 

    } else {
      this.redirecionarHome();

    }
  }

}
