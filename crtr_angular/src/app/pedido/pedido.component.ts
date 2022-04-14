import { Component, OnInit } from '@angular/core';

import { PedidoService } from './service/pedido.service';

@Component({
  selector: 'app-pedido',
  templateUrl: './pedido.component.html',
  styleUrls: ['./pedido.component.scss']
})
export class PedidoComponent implements OnInit {

  constructor(private pedidoService: PedidoService) { }

  ngOnInit(): void {
    this.pedidoService.Pedidos()
      .then(res => {
        console.log(res);
      })
      .catch(err => {
        console.log(err);
      })
      .finally(() => {
        console.log('Fim');
      });
  }
}
