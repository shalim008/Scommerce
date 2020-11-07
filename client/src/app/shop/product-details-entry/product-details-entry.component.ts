import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IBrand } from 'src/app/shared/models/brand';
import { IProductAttribute, IProductAttributeValues, IProductAttrValueMap, IProductVariations } from 'src/app/shared/models/product';
import { IType } from 'src/app/shared/models/productType';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-details-entry',
  templateUrl: './product-details-entry.component.html',
  styleUrls: ['./product-details-entry.component.scss']
})
export class ProductDetailsEntryComponent implements OnInit {

  constructor(private fb: FormBuilder,
              private router: Router,
              private shopService: ShopService) { }

  registerForm: FormGroup;
  errors: string[];
  productEntryForm: FormGroup;
  jsonDoc = null;
  brands: IBrand[];
  types: IType[];
 // productAttrValueMap: IProductAttribute[];
  productAttrValueMap: IProductAttrValueMap[] = [];
  selectedAttributes = [];
  selectedAttribute = '-- None --';
 // productAttributeValues: IProductAttributeValues[] = [];
  productAttributeValue: IProductAttributeValues = null;
  attributeValueSetup = '';
  selectedFiles: FileList;
  productVariations: IProductVariations[] = [];
  // varProductAttrValueMap: IProductAttrValueMap[] = [];
  selectedVarAttribute = '';
  selectedVarAttributeValue = '';
  varProductAttrArray = [];

  blurred = false;
  focused = false;

  // tslint:disable-next-line: typedef
  ngOnInit() {
    this.productForm();
    this.loadProductBrands();
    this.loadProductTypes();
    this.loadProductAttributes();
  }

  // tslint:disable-next-line: typedef
  focus($event: any) {
    // tslint:disable-next-line:no-console
    console.log('focus', $event);
    this.focused = true;
    this.blurred = false;
  }

  // tslint:disable-next-line: typedef
  blur($event: any) {
    // tslint:disable-next-line:no-console
    this.focused = false;
    this.blurred = true;
  }

  // tslint:disable-next-line: typedef
  loadProductTypes() {
    this.shopService.getTypes().subscribe(response => {
      this.types = [{id: 0, name: ''}, ...response];
    }, error => {
      console.log(error);
    });
  }

  // tslint:disable-next-line: typedef
  onKeyBlur(event: any, item) { // without type info
    const d = event.target.value.split(';');

    if (d.length > 0)
    {
      this.productAttrValueMap.forEach( (i, index) => {
          if (i.attributeName === item)
          {
              this.productAttrValueMap[index].productAttributeValues = [];
              d.forEach(element => {
                if (d !== '')
                {
                  this.productAttrValueMap[index].productAttributeValues.push(
                    {
                      id: index,
                      attributeValueName: element,
                      attributeValueParentId: '',
                      productAttributeId: 0
                    });
                }
              });
          }
        });


      const productAttrForm = {
        selectedAttributes: this.productAttrValueMap,
      };
      this.productEntryForm.controls.productAttributeForm.patchValue(productAttrForm);
      console.log(this.productEntryForm.value);
    }
  }

  addExtraProductAttribute(val)
  {
    this.productAttrValueMap.push( {
      id: this.productAttrValueMap.length + 1,
      attributeName: val,
      productAttributeValues: []
    });
  }

  // tslint:disable-next-line: typedef
  addAttributes(val: any){
    if (val !== '-- None --' )
    {
      if (this.selectedAttributes.length > 0)
      {
        this.selectedAttributes.forEach( (item, index) => {
          if (item === val) {
            this.selectedAttributes.splice(index, 1);
            }
        });
      }

      this.selectedAttributes.push(val);
    }
  }

    // tslint:disable-next-line: typedef
    removeAttributes(val: any){
      if (this.selectedAttributes.length > 0)
      {
        this.selectedAttributes.forEach( (item, index) => {
          if (item === val) {
             this.selectedAttributes.splice(index, 1);
             this.productAttrValueMap.forEach( (i, index) => {
              if (i.attributeName === val)
              {
                  this.productAttrValueMap[index].productAttributeValues = [];
              }
            });

          }

        });
      }
    }

  // tslint:disable-next-line: typedef
  loadProductBrands(){
    this.shopService.getBrands().subscribe(response => {
      this.brands = [{id: 0, name: '-- None --'}, ...response];
    }, error => {
        console.log(error);
    });
  }

  // tslint:disable-next-line: typedef
  loadProductAttributes(){
    this.shopService.getAttributeValueMap().subscribe(response => {
      this.productAttrValueMap = [{id: 0, attributeName: '-- None --', productAttributeValues: [] }, ...response];
      console.log(this.productAttrValueMap);
    }, error => {
        console.log(error);
    });

  }

  // tslint:disable-next-line: typedef
  addVaiations(){
    console.log(this.productEntryForm.value.productVariationForm);
    if (this.varProductAttrArray.length > 0)
    {
      this.varProductAttrArray.forEach( (item, index) => {
        if (item.attributeName === this.productEntryForm.value.productVariationForm.varAttr
          && item.attributeValueName ===  this.productEntryForm.value.productVariationForm.varAttrValue) {
           this.varProductAttrArray.splice(index, 1);
        }
      });
    }

    const data = {
      attributeName: this.productEntryForm.value.productVariationForm.varAttr,
      attributeValueName: this.productEntryForm.value.productVariationForm.varAttrValue,
    };

    this.varProductAttrArray.push(data);
  }

   // tslint:disable-next-line: typedef
   onSubmit(param: number) {
     debugger;
     this.shopService.setProductInfo(this.productEntryForm.value, param).subscribe(response => {
      if (response){
        this.productForm();
      // this.toastr.success('User Save Success.');
      }
    }, error => {
      console.log(error);
      this.errors = error.errors;
    });


  }


  selectFeatureImages(event): void {
    if (event.target.files.length > 0) {
      this.selectedFiles = event.target.files;
      const productDetailsForm = {
        pictureFile: this.selectedFiles.item(0),
      };
      this.productEntryForm.controls.productDetailsForm.patchValue(productDetailsForm);
    }
  }
  selectGalleryImages(event): void {
    if (event.target.files.length > 0) {
      this.selectedFiles = event.target.files;
      const productDetailsForm = {
        gallaryImages: this.selectedFiles,
      };
      this.productEntryForm.controls.productDetailsForm.patchValue(productDetailsForm);
    }
  }

  selectVariationImage(event): void {
    if (event.target.files.length > 0) {
      this.selectedFiles = event.target.files;
      const productVarForm = {
        pictureFile: this.selectedFiles.item(0),
      };
      this.productEntryForm.controls.productVariationForm.patchValue(productVarForm);
    }
  }


    // tslint:disable-next-line: typedef
    productForm() {

      this.productEntryForm = this.fb.group({
        productDetailsForm: this.fb.group({
          name: [null, Validators.required],
          description: [null, Validators.required],
          price: ['0', Validators.required],
          regularPrice: ['0', Validators.required],
          productWeight: ['0', Validators.required],
          productLength: ['0', Validators.required],
          productWidth: ['0', Validators.required],
          productHeight: ['0', Validators.required],
          pictureUrl: [null, Validators.required],
          pictureFile: [null],
          productImages: [null],
          gallaryImages: [[]],
          isPublished: [1],
          types: ['0', Validators.required],
          brands: ['0']
        }),
        productInventoryForm: this.fb.group({
          productSKU: [1, Validators.required],
          isManageStock: [false],
          stockQuantity: [1000],
          lowStockThreshold: [10],
        }),
        productAttributeForm: this.fb.group({
          productAttrValueMap: ['0'],
          attributeName: [''],
          isVisibleOnProductPage: [true],
          isUsedForVariations: [false],
          attributeValueName: [null],
          productAttribute: [],
          productAttributeValues: [],
          selectedAttributes: [],
        }),
        productVariationForm: this.fb.group({
          sku: [null, Validators.required],
          description: [null, Validators.required],
          price: [null, Validators.required],
          regularPrice: [null, Validators.required],
          productWeight: ['0', Validators.required],
          productLength: ['0', Validators.required],
          productWidth: ['0', Validators.required],
          productHeight: ['0', Validators.required],
          pictureUrl: [null],
          pictureFile: [null],
          varAttr: [null],
          varAttrValue: [null]
        }),
      });
    }

}
