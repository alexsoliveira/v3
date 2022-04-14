import { DatePipe } from '@angular/common';
import { Injector } from '@angular/core';
import { AbstractControl } from '@angular/forms';

export class ValidacoesCC {
  
  
  static numeroCartao(controle: AbstractControl): any {

    const valor = String(controle.value).replace(/\D+/g, '');

    if (valor.length === 16){
      return null;
    }
    return  {cartaoInvalido: true};
  }

  static validadeCartao(controle: AbstractControl): any {

    const valor = String(controle.value).replace(/\D+/g, '');

    if (valor.length === 6){
      return null;
    }
    return  {dataInvalida: true};
  }

  static cvv(controle: AbstractControl): any {

    const valor = String(controle.value).replace(/\D+/g, '');

    if (valor.length === 3){
      return null;
    }
    return  {cvvInvalido: true};
  }
}
