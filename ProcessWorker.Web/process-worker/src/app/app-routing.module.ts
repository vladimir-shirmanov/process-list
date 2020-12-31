import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {AuthComponent} from "./auth/auth.component";
import {ProcessManagementComponent} from "./process-management/process-management.component";
import {CreateProcessComponent} from "./process-management/create-process/create-process.component";

const routes: Routes = [
  {path: 'login', component: AuthComponent},
  {path: 'process-management', component: ProcessManagementComponent},
  {path: 'create-process', component: CreateProcessComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
