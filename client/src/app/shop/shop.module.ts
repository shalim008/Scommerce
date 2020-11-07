import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopComponent } from './shop.component';
import { ProductItemComponent } from './product-item/product-item.component';
import { SharedModule } from '../shared/shared.module';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { ShopRoutingModule } from './shop-routing.module';
import { NgxGalleryModule } from '@kolkov/ngx-gallery';
import { ProductDetailsEntryComponent } from './product-details-entry/product-details-entry.component';
import { QuillModule } from 'ngx-quill';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    ShopRoutingModule,
    NgxGalleryModule,
    QuillModule.forRoot({
      modules: {
        syntax: false,
        toolbar: [
          ['bold', 'italic', 'underline', 'strike'],        // toggled buttons
          ['blockquote', 'code-block'],
       
          [{ 'header': 1 }, { 'header': 2 }],               // custom button values
          [{ 'list': 'ordered'}, { 'list': 'bullet' }],
          [{ 'script': 'sub'}, { 'script': 'super' }],      // superscript/subscript
          [{ 'indent': '-1'}, { 'indent': '+1' }],          // outdent/indent
          [{ 'direction': 'rtl' }],                         // text direction
       
          [{ 'size': ['small', false, 'large', 'huge'] }],  // custom dropdown
          [{ 'header': [1, 2, 3, 4, 5, 6, false] }],
       
          [{ 'color': [] }, { 'background': [] }],          // dropdown with defaults from theme
          [{ 'font': [] }],
          [{ 'align': [] }],
       
          ['clean'],                                         // remove formatting button
       
          ['link', 'image', 'video']                         // link and image, video

        ]
      }
    })
  ],
  declarations: [ShopComponent, ProductItemComponent, ProductDetailsComponent, ProductDetailsEntryComponent],
})
export class ShopModule { }

