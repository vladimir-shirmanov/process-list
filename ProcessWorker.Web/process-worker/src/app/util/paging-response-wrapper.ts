import {ResponseWrapper} from "./response-wrapper";

export class PagingResponseWrapper<T> extends ResponseWrapper<T>{
  page: number;
  pageSize: number;
  totalItems: number;
}
