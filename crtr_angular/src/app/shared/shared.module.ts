
// * Modules
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxSpinnerModule } from 'ngx-spinner';
import { MatProgressButtonsModule } from 'mat-progress-buttons';
import { TextMaskModule } from 'angular2-text-mask';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MaterialModule } from './material/material.module';
import { SharedRoutingModule } from '../shared/shared.routing';

// * Component
import { BtnVoltarComponent } from './btn-voltar/btn-voltar.component';
import { TopMenuComponent } from './top-menu/top-menu.component';
import { FooterComponent } from './footer/footer.component';
import { FormDebugComponent } from './form-debug/form-debug.component';
import { CarregandoComponent } from './carregando/carregando.component';
import { ModalComponent } from './modal/modal.component';
import { UsuarioMenuComponent } from '../conta/usuario/usuario-menu/usuario-menu.component';
import { CertificadoComponent } from './certificado/components/certificado.component';
import { EnderecoItemComponent } from './endereco-item/endereco-item.component';
import { EnderecoNovoComponent } from './endereco-novo/endereco-novo.component';
import { PoliticaPrivacidadeComponent } from './politica-privacidade/politica-privacidade.component';
import { BannerComponent } from './politica-privacidade/banner/banner.component';

@NgModule({
  declarations: [
    TopMenuComponent,
    BtnVoltarComponent,
    FooterComponent,
    FormDebugComponent,
    CarregandoComponent,
    ModalComponent,
    UsuarioMenuComponent,
    CertificadoComponent,
    EnderecoItemComponent,
    EnderecoNovoComponent,
    PoliticaPrivacidadeComponent,
    BannerComponent
  ],
  imports: [
    CommonModule,
    NgxSpinnerModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    MatProgressButtonsModule,
    TextMaskModule,
    FlexLayoutModule,
    MaterialModule,
    SharedRoutingModule
  ],
  exports: [
    TopMenuComponent,
    BtnVoltarComponent,
    FooterComponent,
    FormDebugComponent,
    CarregandoComponent,
    UsuarioMenuComponent,
    CertificadoComponent,
    EnderecoItemComponent,
    EnderecoNovoComponent,
    PoliticaPrivacidadeComponent,
    MaterialModule,
    SharedRoutingModule,
    BannerComponent
  ],
})
export class SharedModule {}
