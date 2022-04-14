import { TestBed } from '@angular/core/testing';

import { CadastroOutorganteService } from './cadastro-outorgante.service';

describe('CadastroOutorganteService', () => {
  let service: CadastroOutorganteService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CadastroOutorganteService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
