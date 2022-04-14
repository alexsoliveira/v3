/* tslint:disable:no-unused-variable */

import { TestBed, inject, waitForAsync } from '@angular/core/testing';
import { SolicitacaoService } from './solicitacao.service';

describe('Service: Solicitacao', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SolicitacaoService]
    });
  });

  it('should ...', inject([SolicitacaoService], (service: SolicitacaoService) => {
    expect(service).toBeTruthy();
  }));
});
