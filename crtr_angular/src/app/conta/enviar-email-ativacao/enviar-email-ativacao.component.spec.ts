import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { EnviarEmailAtivacaoComponent } from './enviar-email-ativacao.component';

describe('EnviarEmailAtivacaoComponent', () => {
  let component: EnviarEmailAtivacaoComponent;
  let fixture: ComponentFixture<EnviarEmailAtivacaoComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ EnviarEmailAtivacaoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnviarEmailAtivacaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
