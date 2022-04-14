import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { NgxSpinnerService } from 'ngx-spinner';

import { Certificados } from './../models/certificados.model';
import { CertificadoLacuna } from './../models/certificado-lacuna.model'; 
import { environment } from '../../../../environments/environment.prod';
import LacunaWebPKI from 'web-pki';
import { LacunaPkiService } from '../../services/lacuna-pki.service';

@Component({
  selector: 'app-certificado',
  templateUrl: './certificado.component.html',
  styleUrls: ['./certificado.component.scss']
})
export class CertificadoComponent implements OnInit {
  certificadoSelecionado: Certificados;
  habilitarBotao: boolean = false;

  certificados: Array<Certificados>;
  flagMsg: boolean = true;
  extensionId: string = environment.idExtensaoChrome;
  pki: any
  certificatesUploaded: boolean = false;
  certificadoFoiSelecionado: boolean = false;

  constructor(
    private dialogRef: MatDialogRef<CertificadoComponent>,
    private spinner: NgxSpinnerService,
    private lacunaPkiService: LacunaPkiService,
    private changeDetector: ChangeDetectorRef
  ) { }

  ngOnInit(): void {
    this.spinner.show();
    
    
    
    this.pki = new LacunaWebPKI(this.lacunaPkiService.getLacunaWebPkiBinary());
    
    this.listar();
    var intervalSpinner = setInterval(() => {
      if(this.certificatesUploaded) {
        this.spinner.hide();
        clearInterval(intervalSpinner);
      }
    }, 500);

    setInterval(() => {
      if(this.certificadoFoiSelecionado) {
        this.habilitarBotao = true;
        this.spinner.hide();
        this.certificadoFoiSelecionado = false;
      }
    }, 500);
  }

  listar() {
    this.flagMsg = false;
    this.certificados = new Array<Certificados>();
    this.pki.listCertificates({
      success: (certs: CertificadoLacuna[]) => {
        for (var i = 0; i < certs.length; i++) {
          var cert = certs[i];
          this.certificados.push(
            new Certificados(cert.issuerName,
                             cert.subjectName,
                             cert.validityEnd.toString(),
                             cert.pkiBrazil.certificateType,
                             cert.thumbprint)
          );
          console.log(certs[i]);
        }

        this.certificatesUploaded = true;
      },
      error: function (errs) {
        this.certificatesUploaded = true;
        this.toastr.error('Ocorreu um erro ao tentar exibir os certificados!');
        console.log(`Ocorreu um erro ao tentar exibir os certificados!\n\n${errs}`); 
      }
    });

  }

  selecionar(certificado: any): void {
    this.spinner.show();
    this.certificadoSelecionado = certificado;
    this.pki.readCertificate(this.certificadoSelecionado.ThumbPrintBase64).success((certEncoded) => {
      this.certificadoSelecionado.CertificadoBase64 = certEncoded;
      this.certificadoFoiSelecionado = true;
		});
  }

  enviar(): void {
    this.dialogRef.close(this.certificadoSelecionado);
  }

}
