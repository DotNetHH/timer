﻿import { Component } from '@angular/core';
import { Router } from '@angular/router';
@Component({
    selector: 'shared-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.css']
})
export class HeaderComponent {
    private isCollapsed = true;

    constructor(private router: Router) {
    }

    toggleState(): boolean {
        return this.isCollapsed = !this.isCollapsed;
    }    
}
