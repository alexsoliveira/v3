import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { PoliticaPrivacidadeComponent } from './politica-privacidade/politica-privacidade.component';


const routes: Routes = [
  { path: 'politica-privacidade', component: PoliticaPrivacidadeComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SharedRoutingModule { }
