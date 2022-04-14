import { TestBed } from '@angular/core/testing';
import { MatrimonioService } from '../services/matrimonio.services';

describe('CadastroOutorgadoService', () => {
  let service: MatrimonioService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MatrimonioService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
