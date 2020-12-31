export class ResponseWrapper<T> {
  succeeded: boolean;
  message: string;
  errors: string[];
  data: T;
}
