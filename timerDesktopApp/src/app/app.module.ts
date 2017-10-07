import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {
  MatButtonModule, MatButtonToggleModule, MatListModule, MatProgressSpinnerModule,
  MatTabsModule
} from '@angular/material';
import {HttpModule} from '@angular/http';
import {FormsModule} from '@angular/forms';
import { TimerComponent } from './timer/timer.component';
import { ReportComponent } from './report/report.component';
import {TimerService} from './timer.service';

@NgModule({
  declarations: [
    AppComponent,
    TimerComponent,
    ReportComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatButtonToggleModule,
    MatProgressSpinnerModule,
    MatListModule,
    MatTabsModule
  ],
  providers: [TimerService],
  bootstrap: [AppComponent]
})
export class AppModule { }
