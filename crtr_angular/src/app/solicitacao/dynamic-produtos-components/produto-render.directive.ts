import { Directive, ViewContainerRef } from '@angular/core';

@Directive({
  selector: '[dynamicProduto]',
})
export class ProdutoRenderDirective {
  constructor(public viewContainerRef: ViewContainerRef) { }
}