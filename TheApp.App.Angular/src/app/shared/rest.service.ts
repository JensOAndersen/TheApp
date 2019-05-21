import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ShoppingItem } from '../models/shopping-item';
import { Observable, throwError } from 'rxjs';
import { retry, catchError }from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class RestService {

  apiUrl = 'http://localhost:44303';

  constructor(
    private httpClient : HttpClient
  ) { 
    this.GetAllShoppingItems()
      .subscribe((x : ShoppingItem) => {
        console.log(x);
      })
  }

  httpOptions = {
    headers:new HttpHeaders({
      'Content-Type':'application/json'
    })
  };

  GetAllShoppingItems() : Observable<ShoppingItem> {
    return this.httpClient.get<ShoppingItem>(this.apiUrl+'/api/ShoppingItem')
      .pipe(
        retry(1),
        catchError(this.handleError)
      )
  }

  handleError(error){
    let errorMessage = '';
    if(error.error instanceof ErrorEvent){
      errorMessage = error.error.message;
    } else {
      errorMessage = 'Error code: ${error.status}\nMessage: ${error.message}';
    }
    window.alert(errorMessage);
    return throwError(errorMessage);
  }
}
