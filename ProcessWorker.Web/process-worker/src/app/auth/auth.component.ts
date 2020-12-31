import { Component, OnInit } from '@angular/core';
import {LoginModel} from "./login-model";
import {AuthServiceService} from "./auth-service.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss']
})
export class AuthComponent implements OnInit {

  loginModel: LoginModel;

  constructor(private loginService: AuthServiceService, private router: Router) {
    this.loginModel = new LoginModel();
  }

  ngOnInit(): void {
  }

  onSubmit() {
    this.loginService.login(this.loginModel).subscribe(v => this.router.navigate(['process-management']));
  }
}
