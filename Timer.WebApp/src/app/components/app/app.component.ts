import { Component } from '@angular/core';
import { BreadcrumbService } from 'ng2-breadcrumb/ng2-breadcrumb';
import { Router } from "@angular/router";
import { AppSettings } from '../../common/app.settings';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';
  data: any;
  constructor(
      private router: Router,
      private breadcrumbService: BreadcrumbService,
  ) {
      let rootAppSettings = window["APP_SETTINGS"];
      AppSettings.API_ENDPOINT = rootAppSettings.ApiEndpoint;

      breadcrumbService.addFriendlyNameForRoute('/home', '');
  }

  ngOnInit() {        
  }
}
