import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { TareaService } from 'src/app/demo/service/Tareas.service';
import { TareasPendientes } from 'src/app/interfaces/TareasPendientes';
import { catchError, throwError } from 'rxjs';
import { parseISO } from 'date-fns';

@Component({
    templateUrl: './crud.component.html',
    providers: [MessageService]
})
export class CrudComponent implements OnInit {

    tareaDialog: boolean = false;

    deleteTareaDialog: boolean = false;

    submitted: boolean = false;

    cols: any[] = [];

    statuses: any[] = [];

    rowsPerPageOptions = [5, 10, 20];

    tareas: TareasPendientes[] = [];

    tarea = {} as TareasPendientes;



    estado: string = '';

    dateVencimiento: Date | null | undefined;

    constructor(
        private messageService: MessageService,
        private tareaService: TareaService
    ) { }

    ngOnInit() {

        this.cols = [
            { field: 'Título', header: 'Título' },
            { field: 'Descripción', header: 'Descripción' },
            { field: 'FechaCreacion', header: 'Fecha Creación' },
            { field: 'FechaVencimiento', header: 'Fecha Vencimiento' },
            { field: 'Completada', header: 'Estado' }
        ];

        this.statuses = [
            { label: 'COMPLETA', value: 'instock' },
            { label: 'PENDIENTE', value: 'outofstock' }
        ];

        this.tareaService.getAll()
            .pipe(
                catchError((err) => {
                    return throwError(() => err.message || 'Server Error')
                })
            )
            .subscribe(res => {
                this.tareas = [...res];
            });
    }

    openNew() {
        this.tarea = {} as TareasPendientes;
        this.submitted = false;
        this.tareaDialog = true;
    }

    editProduct(tareaPendiente: TareasPendientes) {
        this.tarea = tareaPendiente;
        this.dateVencimiento = parseISO(tareaPendiente.FechaVencimiento)
        this.estado = tareaPendiente.Completada ? 'COMPLETA' : 'PENDIENTE';
        this.tareaDialog = true;
    }

    deleteProduct(tareaPendiente: TareasPendientes) {
        this.deleteTareaDialog = true;
        this.tarea = { ...tareaPendiente };
    }

    confirmDelete() {
        this.tareaService.delete(this.tarea.Id)
            .pipe(
                catchError((err) => {
                    this.messageService.add({ severity: 'error', summary: 'Error!', detail: 'Error al eliminar tarea', life: 3000 });
                    return throwError(() => err.message || 'Server Error')
                })
            )
            .subscribe((res: any) => {
                this.deleteTareaDialog = false;
                this.tareas = this.tareas.filter(f => f.Id !== this.tarea.Id);
                this.tarea = {} as TareasPendientes;
                this.dateVencimiento = null;
                this.messageService.add({ severity: 'success', summary: 'Tarea eliminada!', detail: 'Tarea eliminada correctamente', life: 3000 });
            })
        
    }

    hideDialog() {
        this.tareaDialog = false;
        this.submitted = false;
    }

    formatDateToCustomISO(date: Date): string {
        const year = date.getFullYear();
        const month = String(date.getMonth() + 1).padStart(2, '0'); // getMonth() is zero-based
        const day = String(date.getDate()).padStart(2, '0');
        const hours = String(date.getHours()).padStart(2, '0');
        const minutes = String(date.getMinutes()).padStart(2, '0');
        const seconds = String(date.getSeconds()).padStart(2, '0');
        
        return `${year}-${month}-${day}T${hours}:${minutes}:${seconds}`;
    }

    saveProduct() {
        if (this.tarea.Id > 0) {
            this.tarea.FechaVencimiento = this.formatDateToCustomISO(this.dateVencimiento).substring(0,10);
            this.tarea.Completada = this.estado === "COMPLETA";
            
            this.tareaService.updateTarea(this.tarea)
                .pipe(
                    catchError((err) => {
                        this.messageService.add({ severity: 'error', summary: 'Error!', detail: 'Error al actualizar tarea', life: 3000 });
                        return throwError(() => err.message || 'Server Error')
                    })
                )
                .subscribe((res: any) => {
                    this.tareaDialog = false;
                    this.tarea = {} as TareasPendientes;
                    this.dateVencimiento = null;
                    this.estado = '';
                    this.messageService.add({ severity: 'success', summary: 'Tarea actualizada!', detail: 'Tarea actualizada correctamente', life: 3000 });
                })
        } 
        else 
        {
            let maxId = this.tareas[this.tareas.length - 1];
            let nuevaTarea = {} as TareasPendientes;
            nuevaTarea.Id = 0;
            nuevaTarea.Titulo = this.tarea.Titulo;
            nuevaTarea.Descripcion = this.tarea.Descripcion;
            nuevaTarea.FechaCreacion = this.formatDateToCustomISO(new Date());
            nuevaTarea.FechaVencimiento = this.formatDateToCustomISO(this.dateVencimiento);
            nuevaTarea.Completada = this.estado === "COMPLETA";
    
            this.tareaService.insertTarea(nuevaTarea)
                .pipe(
                    catchError((err) => {
                        this.messageService.add({ severity: 'error', summary: 'Error!', detail: 'Error al guardar tarea', life: 3000 });
                        return throwError(() => err.message || 'Server Error')
                    })
                )
                .subscribe((res: any) => {
                    if (res) {
                        nuevaTarea.Id = maxId === undefined ? 1 : maxId.Id + 1;
                        nuevaTarea.FechaCreacion = nuevaTarea.FechaCreacion.substring(0,10);
                        nuevaTarea.FechaVencimiento = nuevaTarea.FechaVencimiento.substring(0,10);
                        this.tareas.push(nuevaTarea);
                        this.tareas = [...this.tareas]
                        this.tareaDialog = false;
                        this.tarea = {} as TareasPendientes;
                        this.dateVencimiento = null;
                        this.estado = '';
                        this.messageService.add({ severity: 'success', summary: 'Tarea registrada!', detail: 'Tarea agregada correctamente', life: 3000 });
                    }
                })
        }
        
    }
}
