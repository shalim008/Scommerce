import { Component, OnInit } from '@angular/core';
import { IProduct, IProductAttribute, IProductAttributeValues } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';
import { BasketService } from 'src/app/basket/basket.service';
import { NgxGalleryOptions, NgxGalleryImage, NgxGalleryAnimation } from '@kolkov/ngx-gallery';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product: IProduct;
  quantity = 1;
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];
  selectedProductAttributeValues: IProductAttributeValues[] = [];
  isEnabled = false;
  
  constructor(private shopService: ShopService,
              private activateRoute: ActivatedRoute,
              private bcService: BreadcrumbService,
              private basketService: BasketService) {
              this.bcService.set('@productDetails', '');
              }

  ngOnInit(): void {


    this.galleryOptions = [
      {
          width: '600px',
          height: '400px',
          thumbnailsColumns: 4,
          imageAnimation: NgxGalleryAnimation.Slide
      },
      // max-width 800
      {
          breakpoint: 800,
          width: '100%',
          height: '600px',
          imagePercent: 80,
          thumbnailsPercent: 20,
          thumbnailsMargin: 20,
          thumbnailMargin: 20
      },
      // max-width 400
      {
          breakpoint: 400,
          preview: false
      }
  ];


    this.loadProduct();

  //   this.galleryImages = [
  //     {
  //         small: 'https://i.imgur.com/DhKkTrG.jpg',
  //         medium: 'https://i.imgur.com/DhKkTrG.jpg',
  //         big: 'https://i.imgur.com/DhKkTrG.jpg'
  //     },
  //     {
  //         small: 'https://i.imgur.com/KZpuufK.jpg',
  //         medium: 'https://i.imgur.com/KZpuufK.jpg',
  //         big: 'https://i.imgur.com/KZpuufK.jpg'
  //     },
  //     {
  //         small: 'https://i.imgur.com/KZpuufK.jpg',
  //         medium: 'https://i.imgur.com/KZpuufK.jpg',
  //         big: 'https://i.imgur.com/KZpuufK.jpg'
  //     }
  // ];


  }

  // tslint:disable-next-line: typedef
  addItemToBasket(){
     this.basketService.addItemToBasket(this.product, this.quantity, this.selectedProductAttributeValues);
  }

  // tslint:disable-next-line: typedef
  incrementQuantity(){
    this.quantity++;
  }

  // tslint:disable-next-line: typedef
  decrementQuantity(){
    if (this.quantity > 1)
    {
      this.quantity--;
    }
  }

  // tslint:disable-next-line: typedef
  onItemChange(attributeValue: IProductAttributeValues){
    if (this.selectedProductAttributeValues.length > 0)
    {
      this.selectedProductAttributeValues.forEach( (item, index) => {
        if (item.productAttributeId === attributeValue.productAttributeId) {
           this.selectedProductAttributeValues.splice(index, 1);
          }
      });
    }
    this.selectedProductAttributeValues.push(attributeValue);
    if (this.product.productAttribute.length > 0){
      if (this.selectedProductAttributeValues.length === this.product.productAttribute.length){
        this.isEnabled = true;
      }
    }
 }

  // tslint:disable-next-line: typedef
  loadProduct(){
    this.galleryImages = [];
    this.shopService.getProduct(+this.activateRoute.snapshot.paramMap.get('id')).subscribe(product => {
      this.product = product;

      if (this.product.productAttribute.length === 0){
        this.isEnabled = true;
      }
      debugger;
      const baseUrl = window.location.origin + '/';
      this.product.productImages.forEach(element => {
        const feed = {
          small: baseUrl + element.imageThumbUrl,
          medium: baseUrl + element.imageThumbUrl,
          big: baseUrl + element.imageBigUrl,
        };
        this.galleryImages.push(feed);
      });

      this.bcService.set('@productDetails', product.name);
    }, error => {
      console.log(error);
    });
  }

  // tslint:disable-next-line: typedef
  filterAttrValFunction(yourcollection: IProductAttributeValues[], id: number) {
    return yourcollection.filter(i => i.productAttributeId === id);
  }

}
