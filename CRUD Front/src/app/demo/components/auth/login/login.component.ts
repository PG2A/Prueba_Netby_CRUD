import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError, throwError } from 'rxjs';
import { AuthService } from 'src/app/demo/service/Auth.service';
import { Login } from 'src/app/interfaces/Login';
import { LayoutService } from 'src/app/layout/service/app.layout.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styles: [`
        :host ::ng-deep .pi-eye,
        :host ::ng-deep .pi-eye-slash {
            transform:scale(1.6);
            margin-right: 1rem;
            color: var(--primary-color) !important;
        }
    `]
})
export class LoginComponent {

    valCheck: string[] = ['remember'];

    username!: string;
    password!: string;

    constructor(
        public layoutService: LayoutService,
        private authService: AuthService,
        private toastr: ToastrService,
        private _router: Router
    ) { }

    Login() {
        let usuario = {} as Login;
        usuario.Username = this.username;
        usuario.Password = this.password;
  
        this.authService.LoginUsuario(usuario)
          .pipe(
            catchError((err) => {
                this.toastr.error('Error!', 'Ocurrió un problema');
                return throwError(() => err.message || 'Server Error')
            })
          )
          .subscribe((res: any) => {
            window.localStorage.setItem("TOKEN", res.token)
            this.toastr.success('Bienvenido!', 'Usuario logeado correctamente');
            this._router.navigate(['/pages/crud'])
          })
    }
}
