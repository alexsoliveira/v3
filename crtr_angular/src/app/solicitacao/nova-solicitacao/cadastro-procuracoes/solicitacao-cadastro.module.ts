// * Modules
import { NgModule } from '@angular/core';
import { NgxMaskModule, IConfig } from 'ngx-mask';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';
import { SharedModule } from './../../../shared/shared.module';
import { SolicitacaoRoutingModule } from '../../solicitacao.route';

// * Components
import { SolicitacaoCadastroComponent } from './solicitacao-cadastro.component';
import { CadastroOutorganteComponent } from './components/cadastro-outorgante/cadastro-outorgante.component';
import { DatagridOutorganteComponent } from './components/datagrid-outorgante/datagrid-outorgante.component';
import { CadastroOutorgadoComponent } from './components/cadastro-outorgado/cadastro-outorgado.component';
import { DatagridOutorgadoComponent } from './components/datagrid-outorgado/datagrid-outorgado.component';
import { InformacoesImportantesComponent } from './components/informacoes-importantes/informacoes-importantes.component';
import { ProdutoRenderDirective } from '../../dynamic-produtos-components/produto-render.directive';
import { MatrimonioComponent } from '../../dynamic-produtos-components/produtos/matrimonio/matrimonio.component';
import { CompraVendaImoveisComponent } from '../../dynamic-produtos-components/produtos/compra-venda-imoveis/compra-venda-imoveis.component';
import { CadastroTestemunhaComponent } from '../../dynamic-produtos-components/produtos/matrimonio/cadastro-testemunha/cadastro-testemunha.component';
import { DatagridTestemunhaComponent } from '../../dynamic-produtos-components/produtos/matrimonio/datagrid-testemunha/datagrid-testemunha.component';

@NgModule({
  declarations: [
    SolicitacaoCadastroComponent,
    CadastroOutorganteComponent,
    DatagridOutorganteComponent,
    CadastroOutorgadoComponent,
    DatagridOutorgadoComponent,
    InformacoesImportantesComponent,
    ProdutoRenderDirective,
    MatrimonioComponent,
    CompraVendaImoveisComponent,
    CadastroTestemunhaComponent,
    DatagridTestemunhaComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
    SolicitacaoRoutingModule,
    FlexLayoutModule,
    NgxMaskModule.forChild(),
  ],
  exports: [ ],
  entryComponents: [ MatrimonioComponent, CompraVendaImoveisComponent ]
})
export class SolicitacaoCadastroModule { }
