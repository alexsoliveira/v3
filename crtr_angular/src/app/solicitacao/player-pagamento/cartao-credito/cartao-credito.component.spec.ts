import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { CartaoCreditoComponent } from './cartao-credito.component';

describe('CartaoCreditoComponent', () => {
  let component: CartaoCreditoComponent;
  let fixture: ComponentFixture<CartaoCreditoComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ CartaoCreditoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CartaoCreditoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
