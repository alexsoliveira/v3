/* tslint:disable:no-unused-variable */

import { TestBed, inject, waitForAsync } from '@angular/core/testing';
import { LacunaPkiService } from './lacuna-pki.service';

describe('Service: LacunaPki', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LacunaPkiService]
    });
  });

  it('should ...', inject([LacunaPkiService], (service: LacunaPkiService) => {
    expect(service).toBeTruthy();
  }));
});
