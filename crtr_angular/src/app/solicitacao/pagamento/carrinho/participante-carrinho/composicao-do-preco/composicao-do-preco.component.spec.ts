import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { ComposicaoDoPrecoComponent } from './composicao-do-preco.component';

describe('ComposicaoDoPrecoComponent', () => {
  let component: ComposicaoDoPrecoComponent;
  let fixture: ComponentFixture<ComposicaoDoPrecoComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ ComposicaoDoPrecoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ComposicaoDoPrecoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
