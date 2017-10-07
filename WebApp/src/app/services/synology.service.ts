import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response, ResponseContentType } from '@angular/http';
import { DomSanitizer } from '@angular/platform-browser';
import { Observable } from 'rxjs';
import { AppSettings } from '../common/app.settings';
import { ContentHeaders } from "../common/headers";
import 'rxjs/add/operator/map'
 
import { AuthenticationService } from './authentication.service';
 
@Injectable()
export class SynologyService {
    constructor(
        private http: Http,
        private authenticationService: AuthenticationService, private contentHeaders: ContentHeaders, private sanitizer:DomSanitizer) {
    }
 
    getCamPicture(camId: number): Observable<any>  {
        let serverUrl = AppSettings.API_ENDPOINT + '/synology/campicture/' + camId;

        return this.http.get(serverUrl, { headers: this.contentHeaders.headers })
            .map(res => {
                return this.sanitizer.bypassSecurityTrustHtml(res.text());
            })
    }
}