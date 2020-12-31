import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthComponent } from './auth/auth.component';
import { FormsModule } from "@angular/forms";
import { ProcessManagementComponent } from './process-management/process-management.component';
import {HttpClientModule} from "@angular/common/http";
import { CreateProcessComponent } from './process-management/create-process/create-process.component';
import {CommonModule} from "@angular/common";
import { PaginationComponent } from './pagination/pagination.component';

@NgModule({
  declarations: [
    AppComponent,
    AuthComponent,
    ProcessManagementComponent,
    CreateProcessComponent,
    PaginationComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    CommonModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
