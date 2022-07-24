import { Injectable } from '@angular/core';
declare var alertify:any;

@Injectable({
  providedIn: 'root'
})
export class AlertifyService {

  constructor() { }

  message(message:string, options:Partial<AlertifyOptions>){
    alertify.set('notifier','delay',options.delay)
    alertify.set('notifier','position',options.position);
    const msc= alertify["error"](message);

    // if(options.dissmissOthers){
    //   msc.dissmissOther();
    // }
  }

  dissmiss(){
    alertify.dissmissAll();
  }
}

export class AlertifyOptions{
  messageType:MessageType = MessageType.Notify;
  position:Position = Position.ButtomRight;
  delay:number = 3;
  dissmissOthers:boolean = false;

}

export enum MessageType{
  Error = "error",
  Message = "message",
  Notify = "notify",
  Success = "success",
  Warning = "warning"
}

export enum Position{
  TopCenter = "top-center",
  TopRight = "top-right",
  TopLeft = "top-left",
  ButtomRight = "buttom-right",
  ButtomCenter = "buttom-center",
  ButtomLeft = "buttom-left"
}
