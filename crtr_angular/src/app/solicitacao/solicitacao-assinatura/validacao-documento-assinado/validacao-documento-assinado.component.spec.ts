import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ValidacaoDocumentoAssinadoComponent } from './validacao-documento-assinado.component';

describe('ValidacaoDocumentoAssinadoComponent', () => {
  let component: ValidacaoDocumentoAssinadoComponent;
  let fixture: ComponentFixture<ValidacaoDocumentoAssinadoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ValidacaoDocumentoAssinadoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ValidacaoDocumentoAssinadoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
