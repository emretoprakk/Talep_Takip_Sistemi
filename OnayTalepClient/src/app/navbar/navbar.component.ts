import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { filter } from 'rxjs';
import { UserStorageService } from '../services/storage/user-storage.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  userRole: string | null = null;
  isUserLoggedIn: boolean = false;
  isApproverLoggedIn: boolean = false;
  isMenuOpen: boolean = false; // Menü durumu için yeni değişken

  constructor(private userStorageService: UserStorageService, private router: Router) {
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    ).subscribe(() => {
      this.checkUserRole();
    });
  }

  ngOnInit(): void {
    this.checkUserRole();
  }

  checkUserRole(): void {
    const user = this.userStorageService.getUser();
    if (user) {
      this.userRole = user.role;
      this.isUserLoggedIn = this.userStorageService.isUserLoggedIn();
      this.isApproverLoggedIn = this.userStorageService.isApproverLoggedIn();
    } else {
      this.isUserLoggedIn = false;
      this.isApproverLoggedIn = false;
    }
  }

  logout(): void {
    this.userStorageService.clearUser();
    this.isUserLoggedIn = false;
    this.isApproverLoggedIn = false;
    this.router.navigate(['/auth-form']);
  }

  redirectToExternalLink() {
    window.location.href = 'https://www.btc-ag.com.tr/';
  }

  toggleMenu(): void {
    this.isMenuOpen = !this.isMenuOpen;
  }
}