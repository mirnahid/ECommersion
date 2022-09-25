import { Component } from '@angular/core';
import { Router } from '@angular/router';
import {ToastrService} from 'ngx-toastr'
import { AuthService } from './services/common/auth.service';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from './services/ui/custom-toastr.service';
declare var $:any

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  constructor(public authService:AuthService,private toastrService:CustomToastrService,private router:Router){
   authService.indetityCheck();
  }
  
  singOut(){
    localStorage.removeItem("accessToken");
    this.authService.indetityCheck();
    this.router.navigate([""]);
    this.toastrService.message("successfully logout","logout",{
    messageType:ToastrMessageType.Warning,
    position:ToastrPosition.TopRight
    });
  }

}
