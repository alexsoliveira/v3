import { ComponentFactoryResolver, ComponentRef, Directive, Injectable, OnInit, Type, ViewContainerRef } from "@angular/core";
import { ResolveComponent } from "../../dynamic-component/resolve-component.service";
import { ProdutoRenderDirective } from "./produto-render.directive";
import { ActivatedRoute } from '@angular/router';
import { MatrimonioComponent } from "./produtos/matrimonio/matrimonio.component";
import { CompraVendaImoveisComponent } from "./produtos/compra-venda-imoveis/compra-venda-imoveis.component";

@Injectable({
  providedIn: 'root'
})

export class ProdutoResolveComponent extends ResolveComponent {

  public activatedRouter: ActivatedRoute;
  public componentRef: ComponentRef<any>;
  constructor(protected componentFactoryResolver: ComponentFactoryResolver) {
    super(componentFactoryResolver);
  }

  public setComponentOnViewContainer(viewContainerRef: ViewContainerRef, nomeProduto: string): void {
    super.component = this.getComponent(nomeProduto);
    this.loadComponent();

    this.componentFactory = super.getComponentFactory();
    if (this.componentFactory) { 
      viewContainerRef.clear();
      this.componentRef = viewContainerRef.createComponent(this.componentFactory);
    }
  }

  getComponent(nomeProduto: string): Type<any> {
    if (!nomeProduto) {
      nomeProduto = this.activatedRouter.snapshot.params.produto;
    }

    if (nomeProduto) {
      switch (nomeProduto) {
        case 'matrimonio':
        case 'Contrair Matrimonio':
        case 'Contrair Matrimônio':
          return MatrimonioComponent;

        case 'compra-venda-imoveis':
          return CompraVendaImoveisComponent;

        default:
          return null;
      }
    }
  }

}
