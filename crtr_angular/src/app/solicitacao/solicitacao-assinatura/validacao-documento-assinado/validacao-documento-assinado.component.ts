import { Component, Input, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { SolicitacaoAssinaturaService } from '../services/solicitacao-assinatura.service';

@Component({
  selector: 'app-validacao-documento-assinado',
  templateUrl: './validacao-documento-assinado.component.html',
  styleUrls: ['./validacao-documento-assinado.component.scss']
})
export class ValidacaoDocumentoAssinadoComponent implements OnInit {

  @Input() blogDocumentoAssinado : any;
  idMatrimonioDocumento: number;

  constructor(    
    private activatedRoute: ActivatedRoute,
    private validacaoAssinatura: SolicitacaoAssinaturaService,
    private sanitizer: DomSanitizer
  ) { }

  ngOnInit(): void {
    this.idMatrimonioDocumento = this.activatedRoute.snapshot.params.idMatrimonioDocumento;
    if(this.idMatrimonioDocumento)
    {
      this.BuscarDocumentoAssinado(Number(this.idMatrimonioDocumento));
    }    
    
  }

  BuscarDocumentoAssinado(idMatrimonioDocumento: number){
    //metodo Api consultar documento assinado e devolve o blob
    this.validacaoAssinatura.ValidacaoDocumento(idMatrimonioDocumento)
      .subscribe(
        documento => {    
          this.blogDocumentoAssinado = documento.blobAssinaturaDigital;     

          const newWindow = window.open('about:blank', '_blank');
          newWindow.document.write(`<iframe src="data:application/pdf;base64, ${this.blogDocumentoAssinado}" width="100%" height="100%"></iframe>`);

          console.log(documento.blobAssinaturaDigital);
        },
        error => {          
          console.log(error);          
        },
        () => {  }
      );
  }

  validarBotao(): boolean {
    if(this.blogDocumentoAssinado)
      return false    
    return true;
  }

  downloadArquivo(): void {
    const link = document.createElement('a');
    link.href = `data:application/pdf;base64, ${this.blogDocumentoAssinado}`;
    link.download = 'Documento_Assinado.pdf';
    link.click();
    window.URL.revokeObjectURL(this.blogDocumentoAssinado);
    link.remove();
  }

}
