import { Component } from '@angular/core';
import { AppSettings } from '../../common/app.settings';
import * as $ from 'jquery';

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent {
    greetings: string;
    picture: string;
    pictureBytes: any;
    intervallSet: boolean = false;

    constructor() {
    }

    ngOnInit() {
        this.greetings = "keine aktueller Task gestartet";        
    }
}
