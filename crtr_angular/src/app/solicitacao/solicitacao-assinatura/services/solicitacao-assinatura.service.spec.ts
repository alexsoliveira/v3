import { TestBed } from '@angular/core/testing';

import { SolicitacaoAssinaturaService } from './solicitacao-assinatura.service';

describe('SolicitacaoAssinaturaService', () => {
  let service: SolicitacaoAssinaturaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SolicitacaoAssinaturaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
