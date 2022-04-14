import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppGuardGuard } from './app-guard.guard';

const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'home',
          loadChildren: () => import('./vitrine/vitrine.module')
          .then(m => m.VitrineModule)
  },
  { path: 'conta',
          loadChildren: () => import('./conta/conta.module')
          .then(m => m.ContaModule)
  },
  { path: 'solicitacao',
          loadChildren: () => import('./solicitacao/solicitacao.module')
          .then(m => m.SolicitacaoModule),
          canActivate: [AppGuardGuard],
  },
  { path: 'pedido',
          loadChildren: () => import('./pedido/pedido.module')
          .then(m => m.PedidoModule),
          canActivate: [AppGuardGuard],
  },
  { path: 'shared',
          loadChildren: () => import('./shared/shared.module')
          .then(m => m.SharedModule)
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
