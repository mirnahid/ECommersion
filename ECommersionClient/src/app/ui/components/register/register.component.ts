import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  constructor(private formbuilder:FormBuilder) { }
  
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
  onSubmit(data:any){
    this.submited=true;

    if(this.frm.invalid){
      return;
    }
  }

}
