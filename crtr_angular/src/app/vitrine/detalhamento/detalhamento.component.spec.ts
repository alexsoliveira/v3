import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { DetalhamentoComponent } from './detalhamento.component';

describe('DetalhamentoComponent', () => {
  let component: DetalhamentoComponent;
  let fixture: ComponentFixture<DetalhamentoComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ DetalhamentoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DetalhamentoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
