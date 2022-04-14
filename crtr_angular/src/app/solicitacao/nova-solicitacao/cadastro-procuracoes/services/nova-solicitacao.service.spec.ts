import { TestBed } from '@angular/core/testing';

import { NovaSolicitacaoService } from './nova-solicitacao.service';

describe('CadastroOutorganteService', () => {
  let service: NovaSolicitacaoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NovaSolicitacaoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
