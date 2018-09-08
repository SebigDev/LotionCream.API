import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { LoginDto, UsersService } from '../../shared/client-services/typescript-angular-client';
import swal from 'sweetalert2';
import { AuthenticationService } from '../../_services';




@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  encapsulation: ViewEncapsulation.None
})
export class LoginComponent implements OnInit {

  loginDto: LoginDto = {};
  login:any = {};
  user: any;

  constructor(private _router: Router,
              private _userService: UsersService,
              private _authService: AuthenticationService
            ) { }

  ngOnInit() {
  }

  goToRegister(){
    this._router.navigate(['account/register']);
  }

  signin():void{
  
    this._authService.login(this.login)
    .subscribe((response: any) =>{
      swal("Success","You have logged in successfully","success");
      this.goToHome();
    },
    error =>{
      swal("Error","Login failed, Please check your credentials", "error");
    }
  )
  }
  goToHome():void{
    this._router.navigate(['/']);
  }
}
