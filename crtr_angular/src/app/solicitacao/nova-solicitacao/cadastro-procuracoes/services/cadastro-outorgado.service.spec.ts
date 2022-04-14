import { TestBed } from '@angular/core/testing';

import { CadastroOutorgadoService } from './cadastro-outorgado.service';

describe('CadastroOutorgadoService', () => {
  let service: CadastroOutorgadoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CadastroOutorgadoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
