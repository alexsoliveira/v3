// * Modules
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// * Components
import { HomeComponent } from './home/home.component';
import { DetalhamentoComponent } from './detalhamento/detalhamento.component';

const vitrineRouterConfig: Routes = [
    { path: '', component: HomeComponent },
    // { path: 'produto/detalhe/:obj', component: DetalhamentoComponent },
    { path: 'produto/detalhe/:id', component: DetalhamentoComponent },
    { path: 'produto/detalhe', component: DetalhamentoComponent }
];

@NgModule({
    imports: [
        RouterModule.forChild(vitrineRouterConfig)
    ],
    exports: [RouterModule]
})
export class VitrineRoutingModule {}
