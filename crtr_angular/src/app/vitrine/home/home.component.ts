import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

import { VitrineService } from '../services/vitrine.service';

import { Produtos } from '../models/produtos.model';
import { CategoriaProduto } from '../models/categoria-produto.model';
import { ProdutoDTO } from './../models/produto-dto.model';
import { ProdutosModalidades } from '../models/produtosModalidades.model';
import { timeInterval } from 'rxjs/operators';
import { Modalidade } from '../../shared/models/Modalidade.model';
import { ProdutoModalidadesService } from '../../shared/services/ProdutoModalidades.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  categoriaProdutos: CategoriaProduto[] = [];
  modalidades: Modalidade[] = [];

  constructor(
    private vitrineService: VitrineService,
    private modalidadesService: ProdutoModalidadesService,
    private spinner: NgxSpinnerService,
    private produtoDTO: ProdutoDTO,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.spinner.show();
    this.vitrine();
  }



  // TODO: consumir API de produtos do cartório para mostrar na vitrine
  vitrine(): void {
    // if (localStorage.getItem('dadosprodutos') !== null) {
    //   this.produtos = JSON.parse(localStorage.getItem('dadosprodutos'));
    //   this.spinner.hide();
    // }
    // else {


    this.vitrineService.vitrine()
      .subscribe(
        categoriasProdutos => {
          //localStorage.setItem('dadosprodutos', JSON.stringify(produtos));
          this.categoriaProdutos = CategoriaProduto.produtosConfiguration(categoriasProdutos);
          console.log(this.categoriaProdutos);
        },
        error => {
          this.spinner.hide();
          console.log(error);
        },
        () => { this.spinner.hide(); }
      );

    this.modalidadesService.obterModalidades()
      .subscribe(modalidades => {
        this.modalidades = modalidades;
      })

    //}
  }

  detalheProduto(idCategoriaProduto: number, idProduto: number): void {
    let categoria = this.categoriaProdutos.find(c => c.idProdutoCategoria == idCategoriaProduto);

    if (categoria) {
      let produto = categoria.produtos.find(p => p.idProduto == idProduto);
      if (produto) {
        this.produtoDTO.idProduto = produto.idProduto;
        this.produtoDTO.titulo = produto.titulo;
        this.produtoDTO.descricao = produto.descricao;
        this.produtoDTO.blobImagemProduto = produto.produtosImagens[0].strBlobConteudo;
        if (this.modalidades && produto.produtosModalidades) {
          this.produtoDTO.modalidades = Modalidade.getPreecherModalidade(produto.produtosModalidades, this.modalidades);
        }
        this.router.navigate(['/produto/detalhe/' + idProduto]);
      }
    }
  }

}
