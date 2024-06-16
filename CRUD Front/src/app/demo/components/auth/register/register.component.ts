import { Component } from '@angular/core';
import { AuthService } from 'src/app/demo/service/Auth.service';
import { Usuario } from 'src/app/interfaces/Usuario';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { MessageService } from 'primeng/api';
import { ToastrService } from 'ngx-toastr';
import { catchError } from 'rxjs';
import { Router } from '@angular/router';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styles: [`
        :host ::ng-deep .pi-eye,
        :host ::ng-deep .pi-eye-slash {
            transform:scale(1.6);
            margin-right: 1rem;
            color: var(--primary-color) !important;
        }
    `],
    providers: [MessageService]
})
export class RegisterComponent {

    valCheck: string[] = ['remember'];

    username!: string;
    email!: string;
    password!: string;


    constructor(
      public layoutService: LayoutService,
      private authService: AuthService,
      private messageService: MessageService,
      private toastr: ToastrService,
      private _router: Router
    ) { }

    Registrar() {

      let usuario = {} as Usuario;
      usuario.Id = 0;
      usuario.Username = this.username;
      usuario.Email = this.email;
      usuario.Password = this.password;

      this.authService.RegistrarUsuario(usuario)
        .pipe(
          catchError((err) => {
            this.messageService.clear();
            this.messageService.add({ key: 'toast2', severity: 'error', summary: 'Error', detail: 'Ocurrió un problema' });
            return err
          })
        )
        .subscribe((res: any) => {
          this.messageService.clear();
          this.messageService.add({ key: 'toast1', severity: 'success', summary: 'Registro éxitoso', detail: 'Usuario registrado correctamente' });
          this.toastr.success('Registro éxitoso!', 'Usuario registrado correctamente');
          this._router.navigate(['/auth/login'])
        })
    }
}
