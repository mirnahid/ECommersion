import { Injectable } from '@angular/core';
import { async, firstValueFrom, Observable } from 'rxjs';
import { Token } from 'src/app/contracts/token/token';
import { TokenResponse } from 'src/app/contracts/token/tokenReponse';
import { Create_User } from 'src/app/contracts/users/create_user';
import { User } from 'src/app/entities/user';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../ui/custom-toastr.service';
import { HttpClientService } from '../http-client.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpClientService:HttpClientService,private toastrService:CustomToastrService) { }

 async create(user:User):Promise<Create_User>{
   const observable:Observable<Create_User|User>= this.httpClientService.post<Create_User|User>({
    controller:"users"
   },user);

   return await firstValueFrom(observable) as Create_User ;
  }

  async Login(userNameOrEmail:string,password:string,callBackFunction?:()=>void):Promise<any>{
    const observable: Observable<any|TokenResponse>= this.httpClientService.post<any|TokenResponse>({
      controller:"users",
      action:"login"
     },{userNameOrEmail, password});
    
     const token:TokenResponse= await firstValueFrom(observable) as TokenResponse;
if(token){
localStorage.setItem("accessToken",token.token.accessToken);
this.toastrService.message("login success","success",{
  messageType:ToastrMessageType.Success,
  position:ToastrPosition.TopRight
})
}

     callBackFunction();

  }
}
