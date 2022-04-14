import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BoletoUtils {


  getLinkBoleto(identifier: string): string {
    if (identifier) {
        return `${environment.urlBoleto}${identifier}`
    } 
    return `Boleto não localizado!`;
  }

}
