import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import { AppProcess } from './app-process';
import {PagingResponseWrapper} from "../util/paging-response-wrapper";

@Injectable({
  providedIn: 'root'
})
export class ProcessManagementService {

  constructor(private http: HttpClient) {

  }

  getProcesses(page: number): Observable<PagingResponseWrapper<AppProcess[]>> {
    return this.http.get<PagingResponseWrapper<AppProcess[]>>(`/api/AppProcess?page=${page}&pageSize=16`);
  }

  createProcess(process: { name: string }): Observable<number> {
    return this.http.post<number>('/api/AppProcess', process);
  }
}
