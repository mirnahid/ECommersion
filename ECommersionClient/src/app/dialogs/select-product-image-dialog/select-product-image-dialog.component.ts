import { Component, Inject, OnInit, Output } from '@angular/core';
import { async } from '@angular/core/testing';
import { MatCard } from '@angular/material/card';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { NgxSpinnerService } from 'ngx-spinner';
import { SpinnerType } from 'src/app/base/base.component';
import { List_Product_Image } from 'src/app/contracts/list_product_image';
import { DialogService } from 'src/app/services/common/dialog.service';
import { FileUploadOptions } from 'src/app/services/common/file-upload/file-upload.component';
import { ProductService } from 'src/app/services/common/model/product.service';
import { BaseDialog } from '../base/base-dialog';
import { DeleteDialogComponent, DeleteState } from '../delete-dialog/delete-dialog.component';

declare var $:any;

@Component({
  selector: 'app-select-product-image-dialog',
  templateUrl: './select-product-image-dialog.component.html',
  styleUrls: ['./select-product-image-dialog.component.scss']
})
export class SelectProductImageDialogComponent extends BaseDialog<SelectProductImageDialogComponent> implements OnInit  {

  constructor(dialogRef : MatDialogRef<SelectProductImageDialogComponent>,
              @Inject(MAT_DIALOG_DATA) public data:SelectProductImageState|string,private productService:ProductService,
              private spinner:NgxSpinnerService,
              private dialog:DialogService) {
    super(dialogRef)
   }
  
   @Output() options:Partial<FileUploadOptions>={
    accept:".png, .jpg, .jpeg, .gif",
    action:"upload",
    controller:"products",
    explanation:"sekilleri secin",
    isAdminPage:true,
    queryString:`id=${this.data}`
   };

   images:List_Product_Image[];

   async ngOnInit(){
    this.spinner.show(SpinnerType.BallAtom);
    this.images =await this.productService.readImages(this.data as string,()=>this.spinner.hide(SpinnerType.BallAtom));
   }

   async deleteImage(imageId:string,event:any){
    this.dialog.openDialog({
      componentType:DeleteDialogComponent,
      data:DeleteState.Yes,
      afterClosed:async ()=>{
        this.spinner.show(SpinnerType.BallAtom);
        this.productService.deleteImage(this.data as string,imageId,()=>{
          this.spinner.hide(SpinnerType.BallAtom);
          var card=$(event.srcElement).parent().parent();
          card.fadeOut(500);
        })
      }
    });
    
   }

}

export enum SelectProductImageState{
  Close
}
