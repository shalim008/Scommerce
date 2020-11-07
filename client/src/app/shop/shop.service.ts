import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { IPagination, Pagination } from '../shared/models/pagination';
import { IBrand } from '../shared/models/brand';
import { IType } from '../shared/models/productType';
import { map } from 'rxjs/operators';
import { ShopParams } from '../shared/models/shopParams';
import { IProduct, IProductAttribute, IProductAttrValueMap } from '../shared/models/product';
import { of } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  baseUrl = environment.apiUrl;
  products: IProduct[] = [];
  brands: IBrand[] = [];
  types: IType[] = [];
  productAttribute: IProductAttribute[] = [];
  productAttrValueMap: IProductAttrValueMap[] = [];
  pagination = new Pagination();
  shopParams = new ShopParams();

  constructor(private http: HttpClient) {}

  // tslint:disable-next-line: typedef
  getProducts(useCache: boolean) {
    if (useCache === false) {
      this.products = [];
    }

    if (this.products.length > 0 && useCache === true) {
      const pagesReceived = Math.ceil(this.products.length / this.pagination.pageSize);
      if (this.shopParams.pageNumber <= pagesReceived) {
        this.pagination.data =
          this.products.slice((this.shopParams.pageNumber - 1) * this.shopParams.pageSize,
            this.shopParams.pageNumber * this.shopParams.pageSize);

        return of(this.pagination);
      }
    }
    let params = new HttpParams();
    if (this.shopParams.brandId !== 0) {
      params = params.append('brandId', this.shopParams.brandId.toString());
    }
    if (this.shopParams.typeId !== 0) {
      params = params.append('typeId', this.shopParams.typeId.toString());
    }
    if (this.shopParams.search) {
      params = params.append('search', this.shopParams.search);
    }
    params = params.append('sort', this.shopParams.sort);
    params = params.append('pageIndex', this.shopParams.pageNumber.toString());
    params = params.append('pageSize', this.shopParams.pageSize.toString());

    return this.http.get<IPagination>(this.baseUrl + 'products', { observe: 'response', params })
      .pipe(
        map(response => {
          this.products = [...this.products, ...response.body.data];
          this.pagination = response.body;
          return this.pagination;
        })
      );
  }

  // tslint:disable-next-line: typedef
  setShopParams(params: ShopParams) {
    this.shopParams = params;
  }

  // tslint:disable-next-line: typedef
  getShopParams() {
    return this.shopParams;
  }

  // tslint:disable-next-line: typedef
  getProduct(id: number)
  {
    const product = this.products.find(p => p.id === id);
    if (product){
      return of(product);
    }
    return this.http.get<IProduct>(this.baseUrl + 'products/' + id);
  }

  // tslint:disable-next-line: typedef
  getBrands() {
    if (this.brands.length > 0) {
      return of(this.brands);
    }
    return this.http.get<IBrand[]>(this.baseUrl + 'products/brands').pipe(
      map(response => {
        this.brands = response;
        return response;
      })
    );
  }

  // tslint:disable-next-line: typedef
  getTypes() {
    if (this.types.length > 0) {
      return of(this.types);
    }
    return this.http.get<IType[]>(this.baseUrl + 'products/types').pipe(
      map(response => {
        this.types = response;
        return response;
      })
    );
  }

  // tslint:disable-next-line: typedef
  getAttributes() {
    if (this.productAttribute.length > 0) {
      return of(this.productAttribute);
    }
    return this.http.get<IProductAttribute[]>(this.baseUrl + 'products/productAttribute').pipe(
      map(response => {
        this.productAttribute = response;
        return response;
      })
    );
  }


  // tslint:disable-next-line: typedef
  getAttributeValueMap() {
    if (this.productAttrValueMap.length > 0) {
      return of(this.productAttrValueMap);
    }
    return this.http.get<IProductAttrValueMap[]>(this.baseUrl + 'products/productAttribute').pipe(
      map(response => {
        response.forEach(element => {
          this.productAttrValueMap.push(
            {
              id: element.id, attributeName: element.attributeName, productAttributeValues: []
            });
        });
        return this.productAttrValueMap;
      })
    );
  }

    setProductInfo(values: any, param: number) {
      const input = new FormData();
      input.append('name', values.productDetailsForm.name);
      input.append('description', values.productDetailsForm.description);
      input.append('price', values.productDetailsForm.price);
      input.append('regularPrice', values.productDetailsForm.regularPrice);
      input.append('productWeight', values.productDetailsForm.productWeight);
      input.append('productLength', values.productDetailsForm.productLength);
      input.append('productWidth', values.productDetailsForm.productWidth);
      input.append('productHeight', values.productDetailsForm.productHeight);
      input.append('pictureUrl', values.productDetailsForm.pictureUrl);
      input.append('pictureFile', values.productDetailsForm.pictureFile);

      for (let i = 0; i < values.productDetailsForm.gallaryImages.length; i++) {
      input.append('gallaryImages', values.productDetailsForm.gallaryImages[i]);
    }
    
      input.append('productImages', values.productDetailsForm.productImages);
    
      input.append('isPublished', values.productDetailsForm.isPublished);
      input.append('types', values.productDetailsForm.types);
      input.append('brands', values.productDetailsForm.brands);

      if (param === 1){
        debugger;
        return this.http.post(this.baseUrl + 'products/product-info', input).pipe(
          map((response) => {
            if (response) {
              console.log('Sucess');
              return response;
            }
          })
        );
      }

    }


}
