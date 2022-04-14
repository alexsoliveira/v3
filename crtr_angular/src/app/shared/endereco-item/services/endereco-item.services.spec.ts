/* tslint:disable:no-unused-variable */
import { TestBed } from '@angular/core/testing';

import { EnderecoItemService } from './endereco-item.services';

describe('EnderecoItemService', () => {
  let service: EnderecoItemService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EnderecoItemService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});