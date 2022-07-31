import { Directive, ElementRef, HostListener, Input, Output, Renderer2 } from '@angular/core';
import { ProductService } from 'src/app/services/common/model/product.service';
import { EventEmitter } from '@angular/core';
import { SpinnerType } from 'src/app/base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { MatDialog } from '@angular/material/dialog';
import { DeleteDialogComponent,DeleteState } from 'src/app/dialogs/delete-dialog/delete-dialog.component';
import { HttpClientService } from 'src/app/services/common/http-client.service';
import { AlertifyService, MessageType, Position } from 'src/app/services/admin/alertify.service';
import {  HttpErrorResponse } from "@angular/common/http";
import { DialogService } from 'src/app/services/common/dialog.service';

declare var $:any;

@Directive({
  selector: '[appDelete]'
})
export class DeleteDirective {

  constructor(private element:ElementRef,
              private _renderer:Renderer2,
              private spinner:NgxSpinnerService,
              private httpClientService:HttpClientService,
              public dialog:MatDialog,
              private alertify:AlertifyService,
              private dialogService:DialogService)
    {
      const img=_renderer.createElement("img");
      img.setAttribute("src","../../../../../assets/remove_icon.png");
      img.setAttribute("style","cursor:pointer;");
      img.width=25;
      img.height=25;
      _renderer.appendChild(element.nativeElement,img);
    }
    
    @Input() id:string;
    @Input() controller:string;

    @Output() callback:EventEmitter<any> = new EventEmitter();

    @HostListener("click")
   async onclick(){
      this.dialogService.openDialog({
        componentType:DeleteDialogComponent,
        data:DeleteState.Yes,
        afterClosed:async ()=>{
          this.spinner.show(SpinnerType.BallAtom);
          const td:HTMLTableCellElement=this.element.nativeElement;
          this.httpClientService.delete({
            controller:this.controller
          },this.id).subscribe(data=>{
            $(td.parentElement).fadeOut(1000,()=>{
              this.callback.emit();
              this.alertify.message("Mehsul silindi",{
               dissmissOthers:true,
               messageType:MessageType.Success,
               position:Position.TopRight
              });
            });
          },(errorResponse:HttpErrorResponse)=>{
            this.spinner.hide(SpinnerType.BallAtom);
            this.alertify.message("Xeta bas verdi",{
              dissmissOthers:true,
              messageType:MessageType.Success,
              position:Position.TopRight
             });
          });       
        }
      });
      
      // const img:HTMLImageElement=event.srcElement;
      // 
    }

    // openDialog(afterClosed:any): void {
    //   const dialogRef = this.dialog.open(DeleteDialogComponent, {
    //     width: '250px',
    //     data: DeleteState.Yes,
    //   });
  
    //   dialogRef.afterClosed().subscribe(result => {
    //    if(result==DeleteState.Yes){
    //     afterClosed();
    //    }
    //   });
    // }

}
