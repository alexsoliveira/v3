import { AbstractControl } from '@angular/forms';

export class ValidacoesCEP {

  static numero(controle: AbstractControl): any {

    const valor = String(controle.value).replace(/\D+/g, '');

    if (valor.length === 8){
      return null;
    }
    return  {cepInvalido: true};
  }  
}
