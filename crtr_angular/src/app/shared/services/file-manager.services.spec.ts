/* tslint:disable:no-unused-variable */

import { TestBed, inject, waitForAsync } from '@angular/core/testing';
import { FileManagerService } from './file-manager.services';

describe('Service: Endereco', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [FileManagerService]
    });
  });

  it('should ...', inject([FileManagerService], (service: FileManagerService) => {
    expect(service).toBeTruthy();
  }));
});
