import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { PlayerPagamentoComponent } from './player-pagamento.component';

describe('PlayerPagamentoComponent', () => {
  let component: PlayerPagamentoComponent;
  let fixture: ComponentFixture<PlayerPagamentoComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ PlayerPagamentoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlayerPagamentoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
