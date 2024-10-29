import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminComponent } from './admin.component';
import { RequestManagementComponent } from './components/request-management/request-management.component';

const routes: Routes = [
  { path: '', component: AdminComponent },
  { path: 'request-management', component: RequestManagementComponent }

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
