import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserComponent } from './user.component';
import { RequestComponent } from './components/request/request.component';
import { MyRequestsComponent } from './components/my-requests/my-requests.component';

const routes: Routes = [
  { path: '', component: UserComponent },
  { path: 'request', component: RequestComponent },
  { path: 'my-requests', component: MyRequestsComponent },
  
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
