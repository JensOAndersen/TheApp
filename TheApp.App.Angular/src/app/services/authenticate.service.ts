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
      //replace this line once bearer tokens are saved in localstorage, or move it to the methods
      'Authorization' : 'bearer ' + 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJuYmYiOjE1NTgyNjUyOTQsImV4cCI6MTU1ODg3MDA5NCwiaWF0IjoxNTU4MjY1Mjk0fQ.wjt8RaWpGRfT4igmykGckym5OQuj1bHRnrika-gkUMc'
    })
  }
}
