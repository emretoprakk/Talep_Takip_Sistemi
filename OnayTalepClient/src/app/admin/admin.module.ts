import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { AdminComponent } from './admin.component';
import { RequestManagementComponent } from './components/request-management/request-management.component';
import { HttpClientModule } from '@angular/common/http';
import { DemoAngularMaterialModule } from '../DemoAngularMaterialModule';
import { FormsModule } from '@angular/forms';
import { MatSnackBarModule } from '@angular/material/snack-bar';

@NgModule({
  declarations: [
    AdminComponent,
    RequestManagementComponent,
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    FormsModule,
    HttpClientModule,
    DemoAngularMaterialModule,
    MatSnackBarModule
  ]
})
export class AdminModule { }
