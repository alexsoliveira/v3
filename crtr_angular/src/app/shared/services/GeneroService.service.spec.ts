/* tslint:disable:no-unused-variable */

import { TestBed, inject } from '@angular/core/testing';
import { GeneroService } from './GeneroService.service';

describe('Service: ProdutoModalidades', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [GeneroService]
    });
  });

  it('should ...', inject([GeneroService], (service: GeneroService) => {
    expect(service).toBeTruthy();
  }));
});
