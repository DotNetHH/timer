import { Component, OnInit } from '@angular/core';
import {TimerService} from '../timer.service';
import {TasksApi} from '../api/TasksApi';

@Component({
  selector: 'app-timer',
  templateUrl: './timer.component.html',
  styleUrls: ['./timer.component.css']
})
export class TimerComponent implements OnInit {

  state: boolean;
  buttonText: string;
  startTime: number;
  stopTime: number;
  elapsedTime: number;

  constructor(private timerService: TimerService, private timerApi: TasksApi) { }

  ngOnInit() {
    this.state = false;
    this.toggleButtonText();
  }

  onClick() {
    this.state = !this.state;
    this.toggleButtonText();
    if (this.state) {
      this.startTime = Date.now();
      console.log('timer started at: ' + this.startTime);

      this.timerApi.apiTasksStartPost({
        ticketId: 'ticket ID',
        description: 'and a description again',
        timeStamp: new Date
      }).subscribe();
    } else {
      this.stopTime = Date.now();
      console.log('timer stopped at: ' + this.stopTime);

      this.elapsedTime = this.stopTime - this.startTime;

      console.log('the elapsed time is: ' + this.elapsedTime + 'ms');

      this.timerApi.apiTasksStopPost({
        description: 'brand new task',
        ticketId: 'new ID',
        timeStamp: new Date
      }).subscribe();

      this.timerService.stopTask({
        description: 'a new Task',
        ticketId: 'ticket',
        timeStamp: Date.now()
      });
    }
  }

  private toggleButtonText() {
      this.buttonText = this.state ? 'Stop Timer' : 'Start Timer';
  }
}
