import { Component, ViewEncapsulation, OnInit } from '@angular/core';
import { Router } from '@angular/router';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  encapsulation:ViewEncapsulation.None
})
export class AppComponent implements OnInit {
 account: boolean = true;
 subaccount: boolean = false;
  title = 'app';

  constructor(private _router: Router){}


  ngOnInit(): void {
    this.account = true;
    this.subaccount = false;
  }

showAccount(){
 this.subaccount = true;
}

}

