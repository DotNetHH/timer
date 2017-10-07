import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { Observable } from 'rxjs';
import { AppSettings } from '../common/app.settings';
import 'rxjs/add/operator/map'
 
import { AuthenticationService } from './authentication.service';
import { User } from '../models/user.model';
 
@Injectable()
export class UserService {
    constructor(
        private http: Http,
        private authenticationService: AuthenticationService) {
    }
 
    getUsers(): Observable<User[]> {
        // add authorization header with jwt token
        let headers = new Headers({ 'Authorization': 'Bearer ' + this.authenticationService.token });
        let options = new RequestOptions({ headers: headers });
 
        // get users from api
        return this.http.get(AppSettings.API_ENDPOINT+'/login/user', options)
            .map((response: Response) => response.json());
    }
}