import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { ResetarSenhaComponent } from './resetar-senha.component';

describe('ResetarSenhaComponent', () => {
  let component: ResetarSenhaComponent;
  let fixture: ComponentFixture<ResetarSenhaComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ ResetarSenhaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ResetarSenhaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
