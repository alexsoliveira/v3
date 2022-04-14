import { TestBed, inject } from '@angular/core/testing';
import { HtmlSelect } from './HtmlSelect.service';

describe('Service: Endereco', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HtmlSelect]
    });
  });

  it('should ...', inject([HtmlSelect], (service: HtmlSelect) => {
    expect(service).toBeTruthy();
  }));
});
