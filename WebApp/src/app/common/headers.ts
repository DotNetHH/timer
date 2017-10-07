import { Headers } from '@angular/http';
import { Injectable } from "@angular/core";

@Injectable()
export class ContentHeaders {

    constructor() {
        
    }

    get headers(): Headers {

        return new Headers({
            'Content-Type': 'application/json',
            'Accept': 'application/json'
        });
    }
}

