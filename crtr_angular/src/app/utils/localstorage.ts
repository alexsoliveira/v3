import { DateUtils } from "./date.utils";

export class LocalStorageUtils {

  salvarToken(token: string): any {
    localStorage.setItem('_t', token);
  }

  lerToken(): string {
    return localStorage.getItem('_t');
  }

  getBearerToken() {
    let token = this.lerToken();
    return {
      'bearer': token
    }
  }

  removerToken(): any {
    try {
      if (localStorage.getItem('_t')) {
        localStorage.removeItem('_t');
      }
    } catch { }
  }

  isPoliticaPrivacidadeAcordada(): Boolean {
    let politica = localStorage.getItem('_p');
    return politica != null;
  }

  setPoliticaPrivacidade(): void {
    let dateUtils = new DateUtils();
    let aceite = { dataAceitePoliticaPrivacidade: dateUtils.getActualDate() }
    localStorage.setItem('_p', JSON.stringify(aceite));
  }

  salvarUsuario(usuario: any): any {
    localStorage.setItem('_u', JSON.stringify(usuario));
  }

  lerUsuario(): any {
    return JSON.parse(localStorage.getItem('_u'));
  }

  removerUsuario(): any {
    if (localStorage.getItem('_u')) {
      localStorage.removeItem('_u');
    }
  }

  remover(): any {
    localStorage.clear();
  }
}
