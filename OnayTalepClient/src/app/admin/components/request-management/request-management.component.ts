import { HttpClient } from '@angular/common/http';
import { Component, HostListener, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { UserStorageService } from 'src/app/services/storage/user-storage.service';

interface Request {
  id: number;
  description: string;
  approver: string;
  status: 'Pending' | 'Approved' | 'Rejected';
  approvedById: number;
  departmentName: string;
  username: string;
  createdDate: string;
}

@Component({
  selector: 'app-request-management',
  templateUrl: './request-management.component.html',
  styleUrls: ['./request-management.component.css']
})
export class RequestManagementComponent implements OnInit {
  requests: Request[] = [];
  userRole: string;
  userId: number;
  selectedRequestId: number | null = null;

  @ViewChild('requestDetailsDialog') requestDetailsDialog!: TemplateRef<any>;
  dialogRef!: MatDialogRef<any, any> | null;

  constructor(private http: HttpClient, private userStorageService: UserStorageService, private snackBar: MatSnackBar, private dialog: MatDialog) {
    const user = this.userStorageService.getUser();
    this.userRole = user.role;
    this.userId = user.id;
  }

  ngOnInit(): void {
    this.loadRequests();
  }

  loadRequests(): void {
    this.http.get<Request[]>('https://localhost:7115/api/request/get-requests')
      .subscribe(
        data => {
          this.requests = data.filter(request => request.approvedById === this.userId);
          console.log("Loaded requests: ", this.requests);
        },
        error => {
          console.error('Error loading requests', error);
        }
      );
  }

  updateStatus(request: Request, status: 'Pending' | 'Approved' | 'Rejected'): void {
    const updateStatusRequest = {
      requestId: request.id,
      status: status
    };
    console.log("Updating status for request: ", updateStatusRequest);
    this.http.post('https://localhost:7115/api/request/update-status', updateStatusRequest)
      .subscribe(
        response => {
          request.status = status;
          this.snackBar.open('Talep durumu başarıyla güncellendi', 'Kapat', {
            duration: 3000,
          });
          console.log("Updated status successfully");
        },
        error => {
          console.error('Error updating request status', error);
          this.snackBar.open('Talep durumu güncellenirken bir hata oluştu', 'Kapat', {
            duration: 3000,
          });
          console.log("Error details: ", error);
        }
      );
  }

  openRequestDetails(request: Request): void {
    this.selectedRequestId = request.id;
    this.dialogRef = this.dialog.open(this.requestDetailsDialog, {
      data: request,
      width: '600px',
      disableClose: true // Pop-up sadece "İptal" butonuyla kapansın
    });

    this.dialogRef.afterClosed().subscribe(() => {
      this.dialogRef = null; // Pop-up kapandıktan sonra dialogRef'i null yap
    });
  }

  closeDialog(): void {
    if (this.dialogRef) {
      this.dialogRef.close();
    }
  }

  @HostListener('document:click', ['$event'])
  onDocumentClick(event: MouseEvent): void {
    const target = event.target as HTMLElement;
    // Pop-up kapalıyken tıklamaları kontrol et
    if (!this.dialogRef && !target.closest('.request') && !target.closest('.dialog-container')) {
      this.selectedRequestId = null;
    }
  }
}