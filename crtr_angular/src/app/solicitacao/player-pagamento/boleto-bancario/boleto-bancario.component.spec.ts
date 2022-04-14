import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { BoletoBancarioComponent } from './boleto-bancario.component';

describe('BoletoBancarioComponent', () => {
  let component: BoletoBancarioComponent;
  let fixture: ComponentFixture<BoletoBancarioComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ BoletoBancarioComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BoletoBancarioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
