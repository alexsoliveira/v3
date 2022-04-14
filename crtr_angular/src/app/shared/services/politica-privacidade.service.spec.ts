/* tslint:disable:no-unused-variable */

import { TestBed, inject, waitForAsync } from '@angular/core/testing';
import { PoliticaPrivacidadeService } from './politica-privacidade.service';

describe('Service: Endereco', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PoliticaPrivacidadeService]
    });
  });

  it('should ...', inject([PoliticaPrivacidadeService], (service: PoliticaPrivacidadeService) => {
    expect(service).toBeTruthy();
  }));
});
