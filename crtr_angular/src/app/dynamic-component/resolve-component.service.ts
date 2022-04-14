import { ComponentFactory, ComponentFactoryResolver, Type, ViewContainerRef } from '@angular/core';

export abstract class ResolveComponent {

    protected componentFactory: ComponentFactory<any> = null;
    protected component: Type<any> = null;
    
    constructor(protected componentFactoryResolver: ComponentFactoryResolver)
    { }

    loadComponent(){
        if (this.component) {
            this.componentFactory = this.componentFactoryResolver.resolveComponentFactory(this.component);
        }
    }

    getComponentFactory(): ComponentFactory<any> {
        return this.componentFactory;
    }

    abstract setComponentOnViewContainer(viewContainerRef: ViewContainerRef, nomeProduto: string);
    abstract getComponent(nomeProduto: string): Type<any>;
}