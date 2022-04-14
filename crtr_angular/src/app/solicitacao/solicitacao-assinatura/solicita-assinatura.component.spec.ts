import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { SolicitaAssinaturaComponent } from './solicita-assinatura.component';

describe('SolicitacaoAssinaturaComponent', () => {
  let component: SolicitaAssinaturaComponent;
  let fixture: ComponentFixture<SolicitaAssinaturaComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ SolicitaAssinaturaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SolicitaAssinaturaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
