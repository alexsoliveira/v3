// import { HttpClient } from '@angular/common/http';
// import { Injectable } from '@angular/core';

// import { Observable } from 'rxjs';
// import { catchError } from 'rxjs/operators';

// import { Certificados } from './../models/certificados.model';
// import { BaseService } from 'src/app/services/base.service';

// @Injectable({
//   providedIn: 'root'
// })
// export class CertificadoService extends BaseService {

//   constructor(private http: HttpClient) { super(); }

//   // Obter todos os certificados
//   certificado(): Observable<Certificados[]> {
//     return this.http
//         .get<any>(this.UrlService + '/Certificado/Buscar')
//         .pipe(catchError((res: Response) => {
//         return super.serviceErrorNavigate(res, this.router);
//       }));
//   }
// }
