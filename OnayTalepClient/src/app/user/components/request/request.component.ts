import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { UserStorageService } from 'src/app/services/storage/user-storage.service';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-request',
  templateUrl: './request.component.html',
  styleUrls: ['./request.component.css']
})
export class RequestComponent implements OnInit {
  requestForm: FormGroup;
  departments: string[] = [];

  constructor(
    private formBuilder: FormBuilder,
    private userService: UserService,
    private router: Router,
    private userStorageService: UserStorageService,
    private snackBar: MatSnackBar
  ) {
    this.requestForm = this.formBuilder.group({
      description: ['', Validators.required],
      approvedById: ['', Validators.required],
      departmentName: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.loadApprovers();
    this.loadDepartments();
  }

  loadApprovers(): void {
  }

  loadDepartments(): void {
    this.userService.getDepartments().subscribe(
      data => {
        this.departments = data;
      },
      error => {
        console.error('Departmanları yüklerken bir hata oluştu', error);
      }
    );
  }

  onSubmit() {
    if (this.requestForm.valid) {
      const user = this.userStorageService.getUser();

      const request = {
        userId: user.id,
        username: user.username, 
        description: this.requestForm.value.description,
        approvedById: this.requestForm.value.approvedById,
        departmentName: this.requestForm.value.departmentName
      };

      this.userService.addRequest(request).subscribe(
        response => {
          this.snackBar.open('Talep başarıyla eklendi', 'Kapat', {
            duration: 3000,
          });
          this.requestForm.reset(); 
        },
        error => {
          this.snackBar.open('Talep ekleme başarısız oldu', 'Kapat', {
            duration: 3000,
          });
        }
      );
    } else {
      this.snackBar.open('Lütfen tüm alanları doldurun', 'Kapat', {
        duration: 3000,
      });
    }
  }

  onCancel() {
    this.router.navigate(['/']);
  }
}