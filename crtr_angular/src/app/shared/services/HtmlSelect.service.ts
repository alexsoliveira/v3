import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class HtmlSelect {


  getTipoDocumento(): any[]{
    let tipoDocumentos: any[] = [
      { valor: 2, texto: 'CPF' },
      { valor: 6, texto: 'RNE' }
    ];
    return tipoDocumentos;
  }
  getTextoTipoDocumento(value: number): any {
    if (value != undefined && value != null) {
      let obj = JSON.stringify(this.getTipoDocumento().find(r => r.valor == value));
      return obj;
    }
    return '';
  }

  

  getTipoDocumentoProduto(): any[]{
    let tipoDocumentosProduto: any[] = [
      { valor: 0, texto: 'RG' },
      { valor: 1, texto: 'RNE' }
    ];
    return tipoDocumentosProduto;
  }
  getTextoTipoDocumentoProduto(value: number): any {
    if (value != undefined && value != null) {
      let obj = JSON.stringify(this.getTipoDocumentoProduto().find(r => r.valor == value));
      return obj;
    }
    return '';
  }




  getEstadoCivil(): any[]{
    let estadoCivies: any[] = [
      { valor: 0, texto: 'Solteiro(a)' },
      { valor: 1, texto: 'Casado(a)' },
      { valor: 2, texto: 'Viúvo(a)' },
      { valor: 3, texto: 'Divorciado(a)' },
    ];
    return estadoCivies;
  }
  getTextoEstadoCivil(value: number): any {
    if (value != undefined && value != null) {
      let obj = JSON.stringify(this.getEstadoCivil().find(r => r.valor == value));
      return obj;
    }
    return '';
  }



  getRegimeCasamento(): any[]{
    let regimeCasamento: any[] = [
      { valor: 0, texto: 'Comunhão parcial de bens' },
      { valor: 1, texto: 'Comunhão universal de bens' },
      { valor: 2, texto: 'Participação final nos aquestos' },
      { valor: 3, texto: 'Separação absoluta de bens' },
    ];
    return regimeCasamento;
  }
  getTextoRegimeSelecionado(value: number): any {
    if (value != undefined && value != null) {
      let obj = JSON.stringify(this.getRegimeCasamento().find(r => r.valor == value));
      return obj;
    }
    return '';
  }
  



  getSituacao(): any[]{
    let situacao: any[] = [
      { valor: 0, texto: 'Vivo' },
      { valor: 1, texto: 'Falecido' },
      { valor: 2, texto: 'Desconhecido' },
    ];
    return situacao;
  }
  getTextoSituacao(value: number): any {
    if (value != undefined && value != null) {
      let obj = JSON.stringify(this.getSituacao().find(r => r.valor == value));
      return obj;
    }
    return '';
  }



  getParte(): any[]{
    let parte: any[] = [
      { valor: 0, texto: 'Requerente' },
      { valor: 1, texto: 'Noivo(a)' },
    ];
    return parte;
  }
  getTextoParte(value: number): any {
    if (value != undefined && value != null) {
      let obj = JSON.stringify(this.getParte().find(r => r.valor == value));
      return obj;
    }
    return '';
  }
}


