import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserStorageService {
  
  private storageKey = 'user';

  saveUser(user: any): void {
    localStorage.setItem(this.storageKey, JSON.stringify(user));
  }

  getUser(): any {
    const user = localStorage.getItem(this.storageKey);
    return user ? JSON.parse(user) : null;
  }

  clearUser(): void {
    localStorage.removeItem(this.storageKey);
  }

  isUserLoggedIn(): boolean {
    const user = this.getUser();
    return user && user.role === 'User';
  }

  isApproverLoggedIn(): boolean {
    const user = this.getUser();
    return user && user.role !== 'User';
  }
}
