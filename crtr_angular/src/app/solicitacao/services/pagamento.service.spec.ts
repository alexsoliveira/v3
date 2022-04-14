/* tslint:disable:no-unused-variable */

import { TestBed, inject, waitForAsync } from '@angular/core/testing';
import { PagamentoService } from './pagamento.service';

describe('Service: PagamentoService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PagamentoService]
    });
  });

  it('should ...', inject([PagamentoService], (service: PagamentoService) => {
    expect(service).toBeTruthy();
  }));
});
