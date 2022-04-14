import { Pipe, PipeTransform } from '@angular/core';
import { stringToKeyValue } from '@angular/flex-layout/extended/typings/style/style-transforms';

@Pipe({
  name: 'participante'
})
export class ParticipantePipe implements PipeTransform {

  valor: string;

  transform(idTipo: number): string {

    switch (idTipo) {
      case 1:
        this.valor = 'Outorgado';
        break;
      case 2:
        this.valor = 'Outorgante';
        break;
    }

    return this.valor;
  }

}
