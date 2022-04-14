import { SharedModule } from './../shared/shared.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PedidoComponent } from './pedido.component';

import { PedidoRoutingModule } from './pedido.routing';
import { FlexLayoutModule } from '@angular/flex-layout';

@NgModule({
  declarations: [
      PedidoComponent
  ],
  imports: [
    CommonModule,
    PedidoRoutingModule,
    FlexLayoutModule,
    SharedModule
  ]
})
export class PedidoModule { }
