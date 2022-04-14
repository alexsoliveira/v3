import { TestBed } from '@angular/core/testing';

import { SolicitacaoExistenteService } from './solicitacao-existente.service';

describe('SolicitacaoExistenteService', () => {
  let service: SolicitacaoExistenteService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SolicitacaoExistenteService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
