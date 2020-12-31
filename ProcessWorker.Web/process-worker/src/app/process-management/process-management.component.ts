import { Component, OnInit } from '@angular/core';
import {ProcessManagementService} from "./process-management.service";
import {Observable} from "rxjs";
import {AppProcess} from "./app-process";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-process-management',
  templateUrl: './process-management.component.html',
  styleUrls: ['./process-management.component.scss']
})
export class ProcessManagementComponent implements OnInit {

  currentPage: number;
  public processes: AppProcess[];
  public maxItems: number;
  constructor(private service: ProcessManagementService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      if(params['page'] && params['page'] > 0) {
        this.currentPage = params['page'];
      } else {
        this.currentPage = 1;
      }
    });
    this.service.getProcesses(this.currentPage).subscribe(response => {
      this.processes = response.data;
      this.maxItems = response.totalItems;
    });
  }

  onPageChanged(page: number) {
    this.currentPage = page;
    this.service.getProcesses(this.currentPage).subscribe(response => {
      this.processes = response.data;
      this.maxItems = response.totalItems;
    });
  }
}
