import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';
import { IBasket, IBasketItem, Basket, IBasketTotals } from '../shared/models/basket';
import { map } from 'rxjs/operators';
import { IProduct, IProductAttributeValues } from '../shared/models/product';
import { IDeliveryMethod } from '../shared/models/deliveryMethod';
import { stringify } from '@angular/compiler/src/util';

@Injectable({
  providedIn: 'root'
})
export class BasketService {
  baseUrl = environment.apiUrl;

  private basketSource = new BehaviorSubject<IBasket>(null);
  basket$ = this.basketSource.asObservable();

  private basketTotalSource = new BehaviorSubject<IBasketTotals>(null);
  basketTotal$ = this.basketTotalSource.asObservable();
  shipping = 0;
  status: boolean;
  // selectedProdeuctAttribute: string = '';

  constructor(private http: HttpClient) { }

  // tslint:disable-next-line: typedef
  setShippingPrice(deliveryMethod: IDeliveryMethod) {
    this.shipping = deliveryMethod.price;
    this.calculateTotals();
  }

  // tslint:disable-next-line: typedef
  getBasket(id: string) {
    return this.http.get(this.baseUrl + 'basket?id=' + id)
      .pipe(
        map((basket: IBasket) => {
          this.basketSource.next(basket);
          this.calculateTotals();
        })
      );
  }

  setBasket(basket: IBasket) {
    return this.http.post(this.baseUrl + 'basket', basket).subscribe((response: IBasket) => {
      this.basketSource.next(response);
      this.calculateTotals();
    }, error => {
      console.log(error);
    });
  }

  getCurrentBasketValue() {
    return this.basketSource.value;
  }

  // tslint:disable-next-line: typedef
  addItemToBasket(item: IProduct, quantity = 1, selectedProductAttributeValues: IProductAttributeValues[]) {
    const itemToAdd: IBasketItem = this.mapProductItemToBasketItem(item, quantity, selectedProductAttributeValues);
    let basket = this.getCurrentBasketValue();
    if (basket === null) {
      basket = this.createBasket();
    }
    basket.items = this.addOrUpdateItem(basket.items, itemToAdd, quantity);
    this.setBasket(basket);
  }

  // tslint:disable-next-line: typedef
  incrementItemQuantity(item: IBasketItem) {
    const basket = this.getCurrentBasketValue();
    const foundItemIndex = basket.items.findIndex(x => x.id === item.id);
    basket.items[foundItemIndex].quantity++;
    this.setBasket(basket);
  }

  // tslint:disable-next-line: typedef
  decrementItemQuantity(item: IBasketItem) {
    const basket = this.getCurrentBasketValue();
    const foundItemIndex = basket.items.findIndex(x => x.id === item.id);
    if (basket.items[foundItemIndex].quantity > 1) {
      basket.items[foundItemIndex].quantity--;
      this.setBasket(basket);
    } else {
      this.removeItemFromBasket(item);
    }
  }

  removeItemFromBasket(item: IBasketItem) {
  
    const basket = this.getCurrentBasketValue();

    if (basket.items.some(x => x.id === item.id && x.type === item.type)) {

       // basket.items = basket.items.filter(i => i.type !== item.type && i.id === item.id);
        const index: number = basket.items.indexOf(item);
        if (index !== -1) {
          basket.items.splice(index, 1);
        }

        if (basket.items.length > 0) {
        this.setBasket(basket);
      } else {
        this.deleteBasket(basket);
      }
    }
  }


  deleteLocalBasket(id: string) {
    this.basketSource.next(null);
    this.basketTotalSource.next(null);
    localStorage.removeItem('basket_id');
  }

  deleteBasket(basket: IBasket) {
    return this.http.delete(this.baseUrl + 'basket?id=' + basket.id).subscribe(() => {
      this.basketSource.next(null);
      this.basketTotalSource.next(null);
      localStorage.removeItem('basket_id');
    }, error => {
      console.log(error);
    });
  }

  private calculateTotals() {
    const basket = this.getCurrentBasketValue();
    const shipping = this.shipping;
    const subtotal = basket.items.reduce((a, b) => (b.price * b.quantity) + a, 0);
    const total = subtotal + shipping;
    this.basketTotalSource.next({shipping, total, subtotal});
  }

  private addOrUpdateItem(items: IBasketItem[], itemToAdd: IBasketItem, quantity: number): IBasketItem[] {
    debugger;
    if (items.length > 0 )
    {
      if (itemToAdd.productAttributeValues.length > 0)
      {
            let status = false;
            items.forEach(element => {
            if ((element.id === itemToAdd.id) && (element.type === itemToAdd.type)) {
              element.quantity += quantity;
              status = true;
            }
          });
            if (!status)
          {
            itemToAdd.quantity = quantity;
            items.push(itemToAdd);
          }
      }
      else{
        const index = items.findIndex(i => i.id === itemToAdd.id);
        if (index === -1) {
         itemToAdd.quantity = quantity;
         items.push(itemToAdd);
       } else {
         items[index].quantity += quantity;
       }
     }
    }
    else{
      const index = items.findIndex(i => i.id === itemToAdd.id);
      if (index === -1) {
       itemToAdd.quantity = quantity;
       items.push(itemToAdd);
     } else {
       items[index].quantity += quantity;
     }
   }


    // const index = items.findIndex(i => i.id === itemToAdd.id);
    // if (index === -1) {
    //   itemToAdd.quantity = quantity;
    //   items.push(itemToAdd);
    // } else {
    //   items[index].quantity += quantity;
    // }
    return items;
  }



  private createBasket(): IBasket {
    const basket = new Basket();
    localStorage.setItem('basket_id', basket.id);
    return basket;
  }

  // tslint:disable-next-line: max-line-length
  private mapProductItemToBasketItem(item: IProduct, quantity: number, selectedProductAttributeValues: IProductAttributeValues[]): IBasketItem {
    let selectedProdeuctAttribute = '';
   // tslint:disable-next-line: typedef
    selectedProductAttributeValues.forEach(function(item){
      selectedProdeuctAttribute += item.attributeValueName + ' | ';
    });

    return {
      id: item.id,
      productName: item.name,
      price: item.price,
      pictureUrl: item.pictureUrl,
      quantity,
      brand: item.productBrand,
      type: selectedProdeuctAttribute.slice(0, -2),
      productAttributeValues: selectedProductAttributeValues
    };
  }
}
