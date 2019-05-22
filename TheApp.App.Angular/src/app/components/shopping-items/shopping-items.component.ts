import { Component, OnInit } from '@angular/core';
import { RestService } from '../../shared/rest.service';
import { ShoppingItem } from '../../models/shopping-item';

@Component({
  selector: 'app-shopping-items',
  templateUrl: './shopping-items.component.html',
  styleUrls: ['./shopping-items.component.scss']
})
export class ShoppingItemsComponent implements OnInit {

  shoppingItems : ShoppingItem[] = [];

  constructor(
    public restApi:RestService
  ) { }

  ngOnInit() {
    this.loadShoppingItems();
  }

  loadShoppingItems(){
    return this.restApi.GetAllShoppingItems()
    .subscribe((data : any) => {
        this.shoppingItems = data;
        console.log(data);
      })
  }
}
