import { environment } from '../../environments/environment';
import { HttpHeaders, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { throwError, Observable } from 'rxjs';

import { LocalStorageUtils } from '../utils/localstorage';
import { Router } from '@angular/router';


export abstract class BaseService {

  private _router: Router;
  constructor(protected router: Router)
  { 
    this._router = router;
  }

  protected UrlService = `${environment.apiUrl + environment.baseUrl}`;
  
  
  localStorageUtils = new LocalStorageUtils();

  protected ObterHeaderJson(): any {
    return {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*'
      })
    };
  }

  protected ObterHeaderText(): any {
    return {
      headers: new HttpHeaders({
        'Content-Type': 'text/plain; charset=utf-8',
        'Access-Control-Allow-Origin': '*'
      }),
      responseType: 'text'
    };
  }

  protected ObterAuthHeaderJson(): any {
    return {
      headers: new HttpHeaders({
        'Content-Type': 'application/json; charset=utf-8',
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Methods': '*',
        'Authorization': `Bearer ${this.localStorageUtils.lerToken()}`
      })
    };
  }

  protected extractData(response: any): any {
    return response || response.data || {};
  }

  protected serviceError(response: Response | any): Observable<any> {
    const customError: string[] = [];
    

    if (response instanceof HttpErrorResponse) {
      if (response.statusText === 'Unknown Error') {
        customError.push('Ocorreu um erro desconhecido');
        response.error.errors = customError;
      }
    }
    

    console.error(response);
    return throwError(response);
  }

  protected serviceErrorNavigate(response: Response | any, router: Router): Observable<any> {
    const customError: string[] = [];
    
    if (response.status === 401) {
      let localStorage = new LocalStorageUtils();
      localStorage.removerToken();
      localStorage.removerUsuario();
      router.navigate(['/login']);
    }

    if (response instanceof HttpErrorResponse) {
      if (response.statusText === 'Unknown Error') {
        customError.push('Ocorreu um erro desconhecido');
        response.error.errors = customError;
      }
    }
    

    console.error(response);
    return throwError(response);
  }
}
