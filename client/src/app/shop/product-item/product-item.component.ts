import { Component, OnInit, Input } from '@angular/core';
import { IProduct } from 'src/app/shared/models/product';
import { BasketService } from 'src/app/basket/basket.service';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.scss']
})
export class ProductItemComponent implements OnInit {

  isEnabled = true;
  @Input() product: IProduct;

  constructor(private basketService: BasketService) { }

  ngOnInit() {
    if (this.product.productAttribute.length > 0){
      this.isEnabled = false;
    }

  }

  addItemToBasket() {
    this.basketService.addItemToBasket(this.product, 1, []);
   // this.basketService.addItemToBasket(this.product);
  }
}
