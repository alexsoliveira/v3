/* tslint:disable:no-unused-variable */

import { TestBed, inject } from '@angular/core/testing';
import { PessoasService } from './Pessoas.service';

describe('Service: PessoasService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PessoasService]
    });
  });

  it('should ...', inject([PessoasService], (service: PessoasService) => {
    expect(service).toBeTruthy();
  }));
});
