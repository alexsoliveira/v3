/* tslint:disable:no-unused-variable */

import { TestBed, inject, waitForAsync } from '@angular/core/testing';
import { DirecionamentoService } from './direcionamento.service';

describe('Service: DirecionamentoService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [DirecionamentoService]
    });
  });

  it('should ...', inject([DirecionamentoService], (service: DirecionamentoService) => {
    expect(service).toBeTruthy();
  }));
});
