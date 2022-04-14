/* tslint:disable:no-unused-variable */

import { TestBed, inject, waitForAsync } from '@angular/core/testing';
import { SolicitacaoPartesService } from './solicitacaoPartes.service';

describe('Service: SolicitacaoPartes', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SolicitacaoPartesService]
    });
  });

  it('should ...', inject([SolicitacaoPartesService], (service: SolicitacaoPartesService) => {
    expect(service).toBeTruthy();
  }));
});
