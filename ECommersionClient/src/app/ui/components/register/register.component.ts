import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Create_User } from 'src/app/contracts/users/create_user';
import { User } from 'src/app/entities/user';
import { UserService } from 'src/app/services/common/model/user.service';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from 'src/app/services/ui/custom-toastr.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  constructor(private formbuilder:FormBuilder,private userService:UserService,private toastrService:CustomToastrService) { }
  
  frm:FormGroup;
   
  ngOnInit(): void {
    this.frm=this.formbuilder.group({
      namesurname:["",[Validators.required,Validators.maxLength(50),Validators.minLength(3)]],
      username:["",[Validators.required,Validators.maxLength(50),Validators.minLength(3)]],
      email:["",[Validators.required,Validators.maxLength(250),Validators.email]],
      password:["",[Validators.required,Validators.maxLength(250),Validators.minLength(5),Validators.email]],
      confirmpassword:["",[Validators.required,Validators.maxLength(250),Validators.minLength(5),Validators.email]]
    });
  }

  get component(){
    return this.frm.controls;
  }
  submited:boolean=false;
 async onSubmit(data:User){
    this.submited=true;

    // if(this.frm.invalid){
    //   return;
    // }

    const result:Create_User= await this.userService.create(data);

    if(result.succeeded){
      this.toastrService.message(result.message,"user successfully registered",{
       messageType:ToastrMessageType.Success,
       position:ToastrPosition.TopRight
      }); 
    }

    else{
      this.toastrService.message(result.message,"Error!!!",{
        messageType:ToastrMessageType.Error,
        position:ToastrPosition.TopRight
       }); 
    }
  }

}
