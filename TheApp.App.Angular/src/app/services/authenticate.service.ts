import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import {Observable, of} from 'rxjs';
import {map, catchError, tap} from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthenticateService {

  constructor(
    private http : HttpClient
  ) { }

  endpoint : String = 'https://localhost:44303';

  httpOptions = {
    headers:new HttpHeaders({
      'Content-Type' :'application/json',
    })
  }
}
