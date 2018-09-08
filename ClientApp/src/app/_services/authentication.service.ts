import { Injectable } from "@angular/core";
import { Http, Response,Headers, RequestOptions } from "@angular/http";
import "rxjs/add/operator/map";

@Injectable()
export class AuthenticationService {
    userToken: any;
    loginDto: any = {};

    constructor(private http: Http) {
    }

    login(loginDto:any) {
        const headers = new Headers({'Content-Type': 'application/json'});
        const option = new RequestOptions({headers: headers});
        return this.http.post('https://localhost:5001/api/Users/login', loginDto, option)
        .map((response: Response) => {
            let user = response.json();
            if(user) 
              localStorage.setItem('token', user.tokenString);
              this.userToken = user.tokenString;
  
        });
      }
    

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
    }
}