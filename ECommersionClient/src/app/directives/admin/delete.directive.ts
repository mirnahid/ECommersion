import { Directive, ElementRef, HostListener, Input, Output, Renderer2 } from '@angular/core';
import { ProductService } from 'src/app/services/common/model/product.service';
import { EventEmitter } from '@angular/core';
import { SpinnerType } from 'src/app/base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';

declare var $:any;

@Directive({
  selector: '[appDelete]'
})
export class DeleteDirective {

  constructor(private element:ElementRef,
              private _renderer:Renderer2,
              private spinner:NgxSpinnerService,
              private productService:ProductService)
    {
      const img=_renderer.createElement("img");
      img.setAttribute("src","../../../../../assets/remove_icon.png");
      img.setAttribute("style","cursor:pointer;");
      img.width=25;
      img.height=25;
      _renderer.appendChild(element.nativeElement,img);
    }
    
    @Input() id:string;

    @Output() callback:EventEmitter<any> = new EventEmitter();

    @HostListener("click")
   async onclick(){
      this.spinner.show(SpinnerType.BallAtom);
      const td:HTMLTableCellElement=this.element.nativeElement;
      await this.productService.delete(this.id);
      $(td.parentElement).fadeOut(1000,()=>{
        this.callback.emit();
      });
      // const img:HTMLImageElement=event.srcElement;
      // 
    }

}
