import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { UserDto, UsersService } from '../../../shared/client-services/typescript-angular-client';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  encapsulation: ViewEncapsulation.None
})
export class NavMenuComponent implements OnInit {
  user: UserDto;

  constructor(private _router: Router, private _userServices: UsersService) { }

  ngOnInit() {
    this.getUser(14);
  }
 getAccount(){
  this._router.navigate(['account/login']);
 }
getUser(id:number){
  this._userServices.apiUsersGetAllUserByIDGet(id)
  .subscribe((result:UserDto) =>{
    this.user = result;
  })
}


 isloggedin(){
   const token = localStorage.getItem('token');
   return !!token;
 }

 logout(){
   localStorage.removeItem('token');
 }
}
