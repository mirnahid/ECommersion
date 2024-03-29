import { Component, OnInit, Output } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { Create_Product } from 'src/app/contracts/create_product';
import { AlertifyService, MessageType, Position } from 'src/app/services/admin/alertify.service';
import { ProductService } from 'src/app/services/common/model/product.service';
import { EventEmitter } from '@angular/core';
import { FileUploadOptions } from 'src/app/services/common/file-upload/file-upload.component';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class CreateComponent extends BaseComponent implements OnInit {

  constructor(spinner:NgxSpinnerService, private productService:ProductService,private alertify:AlertifyService) {
    super(spinner)
   }

  ngOnInit(): void {
  }
   
  @Output() createdProduct:EventEmitter<Create_Product>=new EventEmitter();
  // @Output() fileUploadOptions:Partial<FileUploadOptions>={
  //   action:"upload",
  //   controller:"products",
  //   explanation:"sekilleri secin",
  //   isAdminPage:true,
  //   accept:".png,.jpg"
  // }

  create(name:HTMLInputElement,stock:HTMLInputElement,price:HTMLInputElement){
    this.showSpinner(SpinnerType.BallAtom);
    const create_product:Create_Product = new Create_Product();
    create_product.name=name.value;
    create_product.price=parseFloat(price.value);
    create_product.stock=parseInt(price.value);

    this.productService.create(create_product,()=>{
      this.hideSpinner(SpinnerType.BallAtom);
      this.createdProduct.emit(create_product);
      this.alertify.message("mehsul elave edildi",{
        dissmissOthers:true,
        messageType:MessageType.Success,
        position:Position.TopRight
      });
     
    },errorMessage=>{
       this.alertify.message(errorMessage,{
        dissmissOthers:true,
        messageType:MessageType.Error,
        position:Position.TopRight
       })
    });
  }
}
