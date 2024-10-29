import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private baseUrl = 'https://localhost:7115/api/request'; 
  private departmentUrl = 'https://localhost:7115/api/department'; 

  constructor(private http: HttpClient) { }

  addRequest(request: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/add`, request);
  }

  getDepartments(): Observable<string[]> {
    return this.http.get<string[]>(`${this.departmentUrl}/get-departments`);
  }

  getUserRequests(userId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/user-requests/${userId}`);
  }


}

export interface RequestDto {
  userId: number;
  description: string;
  approvedById: number;
  departmentName: string;
}
