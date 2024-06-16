import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { Usuario } from '../../interfaces/Usuario';
import { throwError } from 'rxjs';
import { Login } from 'src/app/interfaces/Login';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = `${environment.api}/authentication`;

  constructor(private http: HttpClient) {}

  RegistrarUsuario(usuario: Usuario) {
    const headerOptions = new HttpHeaders({
      'Content-Type': 'application/json'
    });

    const body = JSON.stringify(usuario);

    return this.http
      .post(`${this.apiUrl}/registro`, body, { headers: headerOptions })
      .pipe(catchError(this.errorHandler));
  }

  LoginUsuario(usuario: Login){
    const headerOptions = new HttpHeaders({
      'Content-Type': 'application/json'
    });

    const body = JSON.stringify(usuario);

    return this.http
      .post(`${this.apiUrl}/login`, body, { headers: headerOptions })
      .pipe(catchError(this.errorHandler));
  }

  private errorHandler(error: any) {
    return throwError(() => error.message || 'Server Error')
  }
}
