import { Component, OnInit } from '@angular/core';
import {ProcessManagementService} from "../process-management.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-create-process',
  templateUrl: './create-process.component.html',
  styleUrls: ['./create-process.component.scss']
})
export class CreateProcessComponent implements OnInit {

  process: { name: string } = { name: ''}

  constructor(private service: ProcessManagementService, private router: Router) { }

  ngOnInit(): void {
  }

  onSubmit() {
    this.service.createProcess(this.process)
      .subscribe(res => {
        this.router.navigate(['process-management'])
      });
  }

}
