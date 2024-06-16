import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { Observable, throwError } from 'rxjs';
import { TareasPendientes } from 'src/app/interfaces/TareasPendientes';

@Injectable({
  providedIn: 'root'
})
export class TareaService {
  private apiUrl = `${environment.api}/tareas`;

  constructor(private http: HttpClient) { }

  getAll(): Observable<TareasPendientes[]> {
    return this.http.get<TareasPendientes[]>(`${this.apiUrl}/listar`).pipe(
      map((res: any) => {
        return res.map((tarea: any) => {
          return {
            Id: tarea.id,
            Titulo: tarea.titulo,
            Descripcion: tarea.descripcion,
            FechaCreacion: tarea.fechaCreacion.substring(0, 10),
            FechaVencimiento: tarea.fechaVencimiento.substring(0, 10),
            Completada: tarea.completada
          } as TareasPendientes;
        });
      }),
      catchError(this.errorHandler)
    );
  }

  getTarea(id: number): Observable<TareasPendientes> {
    return this.http.get<TareasPendientes>(`${this.apiUrl}/obtener?id=${id}`).pipe(
      catchError(this.errorHandler)
    );
  }

  insertTarea(tarea: TareasPendientes): Observable<any> {
    return this.http.post<TareasPendientes>(`${this.apiUrl}/guardar`, tarea).pipe(
      catchError(this.errorHandler)
    );
  }

  updateTarea(tarea: TareasPendientes): Observable<any> {
    return this.http.put<TareasPendientes>(`${this.apiUrl}/actualizar`, tarea).pipe(
      catchError(this.errorHandler)
    );
  }

  delete(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/eliminar?id=${id}`).pipe(
      catchError(this.errorHandler)
    );
  }

  private errorHandler(error: any) {
    return throwError(() => error.message || 'Server Error');
  }
}
