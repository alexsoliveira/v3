import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';

import { BaseService } from '../../services/base.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class PedidoService extends BaseService {

  constructor(
    private http: HttpClient,
    public router: Router
  ) { super(router); }

  Pedidos(): any{
    return this.http.get(this.UrlService + '/Conta/teste').toPromise();
  }
}
