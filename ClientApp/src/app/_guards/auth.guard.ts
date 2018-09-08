import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from "@angular/router";
import { Observable } from "rxjs";
import { UsersService } from "../shared/client-services/typescript-angular-client/index";


@Injectable()
export class AuthGuard implements CanActivate {
    constructor(private _router: Router, private _userService: UsersService) {
    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | boolean {
      let user = JSON.parse(localStorage.getItem('token'));
      return this._userService.apiUsersLoginPost().map(
            data => {
                
                if (data !== null) {
                    // logged in so return true
                    return true;
                }
            },
            error => {
               // error when verify so redirect to login page with the return url
               this._router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
               return false;
            });
    }
}
