import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DateUtils {

  getActualDate(){
    let parseDate = new Date();
    let dia = this.formataData(parseDate.getDate().toString());
    let mes = this.formataData((parseDate.getMonth() + 1).toString());
    let ano = parseDate.getFullYear();
    let date = `${dia}/${mes}/${ano}`;

    return date;
  }

  getValidDate(stringDate: string): string {
    if (!stringDate) {
      return '';
    }

    let parseDate = new Date(Number(Date.parse(stringDate)));
    if (!parseDate) {
      return '';
    }

    if (parseDate.getFullYear() === 1) {
      return '';
    }

    let dia = this.formataData(parseDate.getDate().toString());
    let mes = this.formataData((parseDate.getMonth() + 1).toString());
    let ano = parseDate.getFullYear();
    let date = `${dia}/${mes}/${ano}`;

    return date;
  }

  convertDateToBackEnd(stringDate: string): string {
    if (!stringDate) {
      return '';
    }

    let splitDate = stringDate.split('/');
    let date = `${splitDate[2]}-${splitDate[1]}-${splitDate[0]}`;

    return date;
  }

  private formataData(date: string){
    if (date.length > 1) {
      return date;
    }
    return `0${date}`;
  }

}
