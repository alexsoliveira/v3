/* tslint:disable:no-unused-variable */

import { TestBed, inject, waitForAsync } from '@angular/core/testing';
import { ProdutoModalidadesService } from './ProdutoModalidades.service';

describe('Service: ProdutoModalidades', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ProdutoModalidadesService]
    });
  });

  it('should ...', inject([ProdutoModalidadesService], (service: ProdutoModalidadesService) => {
    expect(service).toBeTruthy();
  }));
});
