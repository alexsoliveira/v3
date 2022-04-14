import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { StatusSolicitacaoComponent } from './status-solicitacao.component';

describe('StatusSolicitacaoComponent', () => {
  let component: StatusSolicitacaoComponent;
  let fixture: ComponentFixture<StatusSolicitacaoComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ StatusSolicitacaoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StatusSolicitacaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
