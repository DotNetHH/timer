import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { DatePipe } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { BaseRequestOptions } from '@angular/http';
import { RouterModule } from '@angular/router';
import { Ng2BreadcrumbModule } from 'ng2-breadcrumb/ng2-breadcrumb';
import { Ng2SmartTableModule, LocalDataSource } from 'ng2-smart-table';
import { CollapseDirective } from 'ng2-bootstrap';
import { BootstrapModalModule } from 'angular2-modal/plugins/bootstrap';
import { UiSwitchModule } from 'ng2-ui-switch';
import { ContentHeaders } from "./common/headers";

import { ModalModule } from 'angular2-modal';
import { AppComponent } from './components/app/app.component';
import { HomeComponent } from './components/home/home.component';
import { HeaderComponent } from './components/shared/header/header.component';
import { FooterComponent } from './components/shared/footer/footer.component';
import { LoaderComponent } from './components/shared/loader/loader.component';

@NgModule({
  declarations: [
    CollapseDirective,
    AppComponent,
    HomeComponent,
    HeaderComponent,
    FooterComponent,
    LoaderComponent
  ],
  imports: [
      RouterModule.forRoot([
       { path: '', redirectTo: 'home', pathMatch: 'full' },
       { path: 'home', component: HomeComponent },
       { path: '**', redirectTo: 'home' }
    ]),
    BrowserModule,
    HttpModule,
    Ng2BreadcrumbModule.forRoot(),
    Ng2SmartTableModule,
    ModalModule.forRoot(),
    BootstrapModalModule,
    UiSwitchModule
  ],
  providers: [
        ContentHeaders,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
