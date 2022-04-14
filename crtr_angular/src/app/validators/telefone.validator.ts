import { AbstractControl } from '@angular/forms';

export class ValidacoesTelefone {

  static tel(controle: AbstractControl): any {

    const valor = String(controle.value).replace(/\D+/g, '');

    if (valor.length === 10 || valor.length === 11){
      return null;
    }
    return  {telInvalido: true};
  }
}
