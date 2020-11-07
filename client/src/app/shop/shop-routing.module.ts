import { Component, NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ShopComponent } from './shop.component';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { ProductDetailsEntryComponent } from './product-details-entry/product-details-entry.component';
import { CommonModule } from '@angular/common';

const routes: Routes = [
  {path: '', component: ShopComponent },
  {path: 'product-details-entry', component: ProductDetailsEntryComponent, data: { breadcrumb: {alias: 'Product Details Entry'}}},
  {path: 'productdetails/:id', component: ProductDetailsComponent, data: { breadcrumb: {alias: 'productDetails'}}}
  
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  declarations: [],
  exports: [RouterModule]
})
export class ShopRoutingModule { }
