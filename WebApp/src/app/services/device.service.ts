import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { Observable } from 'rxjs';
import { AppSettings } from '../common/app.settings';
import { ContentHeaders } from "../common/headers";
import { DeviceModel } from "../models/device.model";
import 'rxjs/add/operator/map'
 
import { AuthenticationService } from './authentication.service';
 
@Injectable()
export class DeviceService {
    constructor(
        private http: Http,
        private authenticationService: AuthenticationService, private contentHeaders: ContentHeaders) {
    }
 
    getDevices(): Observable<any> {
        let serverUrl = AppSettings.API_ENDPOINT + '/device/list';

        return this.http.get(serverUrl, { headers: this.contentHeaders.headers })
            .map((response: Response) => {
                //if(response != null && response != undefined && response.json().code == 0) {
                    //console.log(response);
                    return response.json();
                //}
            });
    }

    getDeviceHistory(busIdentifier: string, days: number): Observable<any> {
        let serverUrl = AppSettings.API_ENDPOINT +  '/device/history/' + busIdentifier + "/" + days; 
        console.log(AppSettings.API_ENDPOINT);

        return this.http.get(serverUrl, { headers: this.contentHeaders.headers })
            .map((response: Response) => {
                //if(response != null && response != undefined && response.json().code == 0) {
                    return response.json();
                //}
            });
    }

    switchDevice(device: any): Observable<any> {
        let serverUrl = AppSettings.API_ENDPOINT +  '/device/set/' + device.busIdentifier;
        if(device.value == "1") {
            serverUrl = serverUrl + '/false';
        } else {
            serverUrl = serverUrl + '/true';
        }

        return this.http.get(serverUrl, { headers: this.contentHeaders.headers })
            .map((response: Response) => {
                    return response.json();
            });
    }
}