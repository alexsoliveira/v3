import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DatagridMinhasSolicitacoesComponent } from './datagrid-minhas-solicitacoes.component';

describe('DatagridMinhasSolicitacoesComponent', () => {
  let component: DatagridMinhasSolicitacoesComponent;
  let fixture: ComponentFixture<DatagridMinhasSolicitacoesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DatagridMinhasSolicitacoesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DatagridMinhasSolicitacoesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
