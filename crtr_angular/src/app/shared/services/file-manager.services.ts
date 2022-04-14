import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class FileManagerService {




    manipularReader(readerEvt: any): void {
        const binaryString = readerEvt.target.result;
        this.imagemBase64 = btoa(binaryString);
        this.documentoAssinado = btoa(binaryString);
      }

      
  

}


