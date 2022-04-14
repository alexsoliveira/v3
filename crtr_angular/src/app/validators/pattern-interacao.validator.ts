import { Injectable } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class PatternInteracaoValidator {

  public customPatternsDocumento = { '0': { pattern: new RegExp('\[0-9\]') } };
  public customPatternsRg = { '0': { pattern: new RegExp('\[A-Za-z0-9\]') } };
  public customPatternsNome = { '0': { pattern: new RegExp('\[A-Za-z \]') } };
  public customPatternsEmail = { '0': { pattern: new RegExp('\[a-z0-9._%+-@\]') } };
  public customPatternsNacionalidade = { '0': { pattern: new RegExp('\[A-Za-z \]') } };
  public customPatternsProfissao = { '0': { pattern: new RegExp('\[A-Za-z \]') } };
  public customPatternsCelular = { '0': { pattern: new RegExp('\[0-9()+-\]') } };
  public customPatternsTelefoneAlternativo = { '0': { pattern: new RegExp('\[0-9()+-\]') } };

  verificarValidTouched(formulario: FormGroup, campo: string): boolean {
    return !formulario.get(campo).valid && (formulario.get(campo).dirty);
  }

  interagiuComControle(formulario: FormGroup, campo: string): void {
    if (this.verificarValidTouched(formulario, campo)) {
      formulario.get(campo).setValidators(Validators.required);
      type NewType = AbstractControl;

      let control: NewType = null;
      control = formulario.controls[campo];
      control.updateValueAndValidity();
    }
  }

  atualizarValidacoesCampos(formulario: FormGroup): void {
    let control: AbstractControl = null;
    Object.keys(formulario.controls).forEach((name) => {
      control = formulario.controls[name];
      control.updateValueAndValidity();
    });
  }

  resetarCampos(formulario: FormGroup, camposExcecao: string[] = null): void {
    let control: AbstractControl = null;
    Object.keys(formulario.controls).forEach((name) => {
      if (!camposExcecao || (camposExcecao && camposExcecao.find(c => c !== name))){
        control = formulario.controls[name];
        control.reset();
        control.clearValidators();
        control.updateValueAndValidity();
      }
    });
  }

  validarDataPadraoBrasileiro(formulario: FormGroup, campo: string): boolean{    
    let data: string = formulario.get(campo).value;
    let dataArray = data.split("/");

    if(dataArray == null)
      return false;
    
    let dia = Number(dataArray[0]);
    let mes = Number(dataArray[1]);
    let ano = Number(dataArray[2]);

    if(mes < 1 || mes > 12)
      return false;
    else if (dia < 1 || dia > 31)
      return false;
    else if ((mes == 4 || mes == 6 || mes == 9 || mes == 11) && dia == 31)
      return false;
    else if (mes == 2)
    {
      let anoBissexto = (ano % 4 == 0 && (ano % 100 != 0 || ano % 400 == 0));
      if(dia > 29 || (dia == 29 && !anoBissexto))
        return false;
    } 

    return true;    
  }    

  validarDataMinimo(formulario: FormGroup, campo: string): boolean{
    let data: string = formulario.get(campo).value;
    let dataArray = data.split("/");

    if(dataArray == null)
      return false;
        
    let ano = Number(dataArray[2]);

    if(ano < 1900){
      return false
    }
    return true;
  }

  validarDataMaxima(formulario: FormGroup, campo: string): boolean{
    let data: string = formulario.get(campo).value;
    let dataArray = data.split("/");
    
    if(dataArray == null)
      return false;
        
    let dia = Number(dataArray[0]);
    let mes = Number(dataArray[1]);
    let ano = Number(dataArray[2]);

    let dataAtual = new Date();
    
    if(ano > dataAtual.getFullYear()){
      return false;
    }

    if((mes > (dataAtual.getMonth()+1)) && ano == dataAtual.getFullYear()){
      return false;
    }

    if(dia > dataAtual.getDate() && (mes == (dataAtual.getMonth()+1)) && ano == dataAtual.getFullYear()){
      return false;
    }

    return true;
  }

}
