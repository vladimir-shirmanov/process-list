import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {LoginModel} from "./login-model";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class AuthServiceService {

  constructor(private httpService: HttpClient) { }

  login(loginModel: LoginModel): Observable<any> {
    return this.httpService.post('/api/Authorization', loginModel)
  }
}
