import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { UsersService, RegisterDto} from '../../shared/client-services/typescript-angular-client';
import swal from 'sweetalert2';



@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  encapsulation: ViewEncapsulation.None
})
export class RegisterComponent implements OnInit {
  createUser: any = {};
  registerDto: RegisterDto;
  today = new Date();
  register: any;
 


  constructor(private _router: Router,
            private _userServices: UsersService,
  ) { }

  ngOnInit() {
  }

  signUp(){
    this.createUser.isActive = true;
    this.createUser.registerDate = this.createUser.registerDate;
    this.createUser.dateOfBirth = this.createUser.dateOfBirth;
    this.registerDto = this.createUser;
    console.log(this.registerDto);
    this._userServices.apiUsersRegisterPost(this.registerDto)
    .subscribe((result:any) =>
    {
      this.register = result;
      console.log(result);
      swal("Success","Your Registration was successful","success");
    })
}
  
goToLogin(){
  this._router.navigate(['account/login']);
  }

}



