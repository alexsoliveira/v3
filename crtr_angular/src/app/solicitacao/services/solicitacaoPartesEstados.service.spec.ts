/* tslint:disable:no-unused-variable */

import { TestBed, inject, waitForAsync } from '@angular/core/testing';
import { SolicitacaoPartesEstadosService } from './solicitacaoPartesEstados.service';

describe('Service: SolicitacaoPartesEstados', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SolicitacaoPartesEstadosService]
    });
  });

  it('should ...', inject([SolicitacaoPartesEstadosService], (service: SolicitacaoPartesEstadosService) => {
    expect(service).toBeTruthy();
  }));
});
