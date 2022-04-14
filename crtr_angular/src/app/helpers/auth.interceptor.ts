import { HttpHandler, HttpInterceptor, HttpRequest, HTTP_INTERCEPTORS } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { LocalStorageUtils } from '../utils/localstorage';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  localStorageUtils = new LocalStorageUtils();

  constructor() {  }

  intercept(req: HttpRequest<any>, next: HttpHandler): any{

    let auth = req;
    const token = this.localStorageUtils.lerToken();

    if (token != null){
      auth = req.clone({headers: req.headers.set('Authorization', `Bearer ${token}`)});
    }
    return next.handle(auth);
  }
}

export const authInterceptorProviders = [
  { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
];


