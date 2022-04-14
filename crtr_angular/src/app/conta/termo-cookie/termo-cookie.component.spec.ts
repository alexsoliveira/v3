import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TermoCookieComponent } from './termo-cookie.component';

describe('TermoCookieComponent', () => {
  let component: TermoCookieComponent;
  let fixture: ComponentFixture<TermoCookieComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TermoCookieComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TermoCookieComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
