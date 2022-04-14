import { FormGroup, FormControl, FormGroupDirective, NgForm } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';

export class ParentErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = !!(form && form.submitted);
    const controlTouched = !!(control && (control.dirty || control.touched));
    const controlInvalid = !!(control && control.invalid);
    const parentInvalid = !!(control && control.parent && control.parent.invalid && (control.parent.dirty || control.parent.touched));

    return controlTouched && (controlInvalid || parentInvalid);

    // return isSubmitted || (controlTouched && (controlInvalid || parentInvalid));
  }
}
export class MatchValidator {
  static areEqual(formGroup: FormGroup) {
    let value;
    let valid = true;
    for (let key in formGroup.controls) {
      if (formGroup.controls.hasOwnProperty(key)) {
        let control: FormControl = <FormControl>formGroup.controls[key];

        if (value === undefined) {
          value = control.value
        } else {
          if (value !== control.value) {
            valid = false;
            break;
          }
        }
      }
    }

    if (valid) {
      return null;
    }

    return {
      areEqual: true
    };
  }
}

export const regExps: { [key: string]: RegExp } = {
  senha: /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]+$/
};

export const ErrorMessages = {
  'nome': [
    { type: 'required', message: 'Campo obrigatório' },
    { type: 'minlength', message: 'O nome de usuário deve ter pelo menos 5 caracteres' },
    { type: 'maxlength', message: 'O nome de usuário não pode ter mais de 15 caracteres' },
    { type: 'pattern', message: 'O nome de usuário deve conter apenas números e letras' },
    { type: 'validUsername', message: 'O nome de usuário já existe' }
  ],
  'email': [
    { type: 'required', message: 'E-mail é obrigatório' },
    { type: 'email', message: 'Entre com um email válido' }
  ],
  'confirma_email': [
    { type: 'required', message: 'E-mail é obrigatório' },
    { type: 'areEqual', message: 'Os endereços de e-mail não conferem' }
  ],
  'senha': [
    { type: 'required', message: 'Informe a senha' },
    { type: 'minlength', message: 'A senha deve conter no mínimo 6 caracteres' },
    { type: 'pattern', message: 'A senha deve conter pelo menos uma letra maiúscula, um número e um caractere especial' }
  ],
  'novaSenha': [
    { type: 'required', message: 'Informe a nova senha' },
    { type: 'minlength', message: 'A nova senha deve conter no mínimo 6 caracteres' },
    { type: 'pattern', message: 'A nova senha deve conter pelo menos uma letra maiúscula, um número e um caractere especial' }
  ],
  'confirma_senha': [
    { type: 'required', message: 'É necessário confirmar a senha' },
    { type: 'areEqual', message: 'As senhas não conferem' }
  ],
  'termos': [
    { type: 'pattern', message: 'Você deve aceitar os termos e condições' }
  ]
};
