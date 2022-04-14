import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { TermoConcordanciaComponent } from './termo-concordancia.component';

describe('TermoConcordanciaComponent', () => {
  let component: TermoConcordanciaComponent;
  let fixture: ComponentFixture<TermoConcordanciaComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ TermoConcordanciaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TermoConcordanciaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
