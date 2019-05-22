import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import {RouterModule, Routes} from '@angular/router'
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { HttpClientModule } from '@angular/common/http';
import { MenuComponent } from './components/menu/menu.component';
import { ShoppingItemsComponent } from './components/shopping-items/shopping-items.component';

const appRoutes : Routes =[
  {path : 'shopping-items', component:ShoppingItemsComponent}
]

@NgModule({
  declarations: [
    AppComponent,
    MenuComponent,
    ShoppingItemsComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule.forRoot(
      appRoutes
    )
    // ,
    // AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
