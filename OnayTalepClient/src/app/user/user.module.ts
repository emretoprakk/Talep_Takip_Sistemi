import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';
import { UserComponent } from './user.component';
import { RequestComponent } from './components/request/request.component';
import { HttpClientModule } from '@angular/common/http';
import { DemoAngularMaterialModule } from '../DemoAngularMaterialModule';
import { FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MyRequestsComponent } from './components/my-requests/my-requests.component';

@NgModule({
  declarations: [
    UserComponent,
    RequestComponent,
    MyRequestsComponent,
  ],
  imports: [
    CommonModule,
    UserRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    DemoAngularMaterialModule,
    MatSnackBarModule
  ]
})
export class UserModule { }
