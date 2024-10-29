import { Component } from '@angular/core';
import { AuthService } from '../services/auth/auth.service';
import { Router } from '@angular/router';
import { UserStorageService } from '../services/storage/user-storage.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-auth-form',
  templateUrl: './auth-form.component.html',
  styleUrls: ['./auth-form.component.css']
})
export class AuthFormComponent {
  username: string = '';
  email: string = '';
  password: string = '';
  isSignDivVisable: boolean = false;

  constructor(
    private authService: AuthService,
    private router: Router,
    private userStorageService: UserStorageService,
    private snackBar: MatSnackBar

  ) { }

  register() {
    this.authService.register(this.username, this.email, this.password).subscribe(
      response => {
        this.isSignDivVisable = false;
        this.snackBar.open('Registration successful', 'Close', {
          duration: 3000,
        });
      },
      error => {
        this.snackBar.open('Registration failed', 'Close', {
          duration: 3000,
        });
      }
    );
  }

  login() {
    this.authService.login(this.email, this.password).subscribe(
      response => {
        const decodedToken = this.authService.getDecodedToken();
        const user = {
          id: response.userId,  
          username: decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'],
          email: decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'],
          role: decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']
        };
        this.userStorageService.saveUser(user);
        console.log('Kullanici:', user);
        
        if (user.role === 'User') {
          this.router.navigate(['/user/request']);
        } else {
          this.router.navigate(['/admin/request-management']);
        }
      },
      error => {
        this.snackBar.open('Login failed', 'Close', {
          duration: 3000,
        });
      }
    );
  }
}