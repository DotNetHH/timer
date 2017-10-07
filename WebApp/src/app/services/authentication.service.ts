import { Injectable } from '@angular/core';
import { Http, Headers, Response } from '@angular/http';
import { Observable } from 'rxjs';
import { AppSettings } from '../common/app.settings';
import { LoginModel } from '../models/login.model';
import * as jwt_decode from 'jwt-decode';
import 'rxjs/add/operator/map'
 
@Injectable()
export class AuthenticationService {
    public token: string;
 
    constructor(private http: Http) {
        // set token if saved in local storage
        var currentUser = JSON.parse(localStorage.getItem('currentUser'));
        this.token = currentUser && currentUser.token;
        console.log("Token: " + this.token);
    }

    getToken(): string {
        return this.token;
      }

    getCurrentUser(): any {
        var currentUser = JSON.parse(localStorage.getItem('currentUser'));
        console.log(currentUser);
        if(currentUser === undefined) {
            return null;
        }
        return currentUser;
    }
    
      setToken(token: string, username: string): void {
        localStorage.removeItem('currentUser');
        localStorage.setItem('currentUser', JSON.stringify({ username: username, token: token }));
      }
    
      getTokenExpirationDate(token: string): Date {
        const decoded = jwt_decode(token);

        if (decoded.exp === undefined) return null;
    
        const date = new Date(0); 
        date.setUTCSeconds(decoded.exp);
        return date;
      }
    
      isTokenExpired(token?: string): boolean {
        if(!token) token = this.getToken();
        if(!token) return true;
    
        const date = this.getTokenExpirationDate(token);

        if(date === undefined) return false;
        return !(date.valueOf() > new Date().valueOf());
      }
  
    login(username: string, password: string): Observable<boolean> {
        let serverUrl = AppSettings.API_ENDPOINT + '/login';

        let login: LoginModel = new LoginModel();
        login.username = username;
        login.password = password;

        return this.http.post(serverUrl, login)
            .map((response: Response) => {
                console.log(response);
                if(response !== null && response !== undefined && response.json().code === 0) {
                    // login successful if there's a jwt token in the response
                    let token = response.json() && response.json().data.token;
                    if (token) {
                        // set token property
                        this.token = token;
                        console.log(response.json().data.firstname);
    
                        // store username and jwt token in local storage to keep user logged in between page refreshes
                        localStorage.setItem('currentUser', JSON.stringify({ username: username, token: token, firstname: response.json().data.firstname, lastname: response.json().data.lastname}));
    
                        // return true to indicate successful login
                        return true;
                    } else {
                        // return false to indicate failed login
                        return false;
                    }
                }
                else {
                    return false;
                }
            });
    }
 
    logout(): void {
        // clear token remove user from local storage to log user out
        this.token = null;
        localStorage.removeItem('currentUser');
    }
}