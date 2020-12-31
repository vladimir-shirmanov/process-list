import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import * as _ from 'underscore';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.scss']
})
export class PaginationComponent implements OnInit {

  pages: number[];
  maxPages: number;

  private _maxItems: number;

  get maxItems(): number {
    return this._maxItems;
  }

  @Input() currentPage: number;
  @Input()
  set maxItems(val: number) {
    this._maxItems = val;
    this.maxPages = Math.ceil(this.maxItems / this.itemsOnPage);
    this.initPages();
  }
  @Input() itemsOnPage: number;
  @Output() pageChanged: EventEmitter<number> = new EventEmitter<number>();


  constructor() {
  }

  private initPages() {
    let min: number;
    let max: number = this.maxPages;
    if(this.currentPage - 2 > 0) {
      min = this.currentPage - 2;
      max = this.currentPage + 2 >= max ? max : this.currentPage + 2;
    } else {
      min = 1;
      max = 5 > max ? max : 5;
    }

    this.pages = _.range(min, max+1);
  }


  ngOnInit(): void {
    if(!this.currentPage) {
      this.currentPage = 1;
    }
  }

  inc(dif: number) {
    this.currentPage = this.currentPage+dif;
    this.initPages();
    this.pageChanged.emit(this.currentPage);
  }

  nav(page: number) {
    this.currentPage = page;
    this.initPages();
    this.pageChanged.emit(this.currentPage);
  }

}
