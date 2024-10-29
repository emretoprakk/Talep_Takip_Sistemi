import { Component, OnInit } from '@angular/core';
import { UserStorageService } from 'src/app/services/storage/user-storage.service';
import { UserService } from '../../services/user.service';


@Component({
  selector: 'app-my-requests',
  templateUrl: './my-requests.component.html',
  styleUrls: ['./my-requests.component.css']
})
export class MyRequestsComponent implements OnInit {
  requests: any[] = [];
  userId: number;
  userRole: string;

  constructor(
    private userService: UserService,
    private userStorageService: UserStorageService
  ) {
    const user = this.userStorageService.getUser();
    this.userId = user.id;
    this.userRole = user.role;
  }

  ngOnInit(): void {
    this.loadUserRequests();
  }

  loadUserRequests(): void {
    this.userService.getUserRequests(this.userId).subscribe(
      data => {
        this.requests = data;
      },
      error => {
        console.error('Error loading user requests', error);
      }
    );
  }

  getApproverName(approvedById: number): string {
    switch(approvedById) {
      case 47:
        return 'Admin';
      case 50:
        return 'Team Leader';
      case 51:
        return 'Director';
      default:
        return 'Unknown';
    }
  }
}


