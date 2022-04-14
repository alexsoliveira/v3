// * Modules
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VitrineRoutingModule } from './vitrine.route';
import { NgxSpinnerModule } from 'ngx-spinner';
import { SharedModule } from './../shared/shared.module';

// * Components
import { HomeComponent } from './home/home.component';
import { DetalhamentoComponent } from './detalhamento/detalhamento.component';
import { MaterialElevationDirective } from '../shared/mdc.elevation.directive';

// * Utils
import { TruncatePipe } from './../utils/truncate.pipe';

@NgModule({
  declarations: [
    HomeComponent,
    DetalhamentoComponent,
    TruncatePipe,
    MaterialElevationDirective
  ],
  imports: [
    CommonModule,
    NgxSpinnerModule,
    SharedModule,
    VitrineRoutingModule
  ]
})
export class VitrineModule { }
