import { Component, OnInit } from '@angular/core';
import {TimerService} from '../timer.service';
import {Task} from '../Model/task.model';

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

  constructor(private timerService: TimerService) { }

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
    } else {
      this.stopTime = Date.now();
      console.log('timer stopped at: ' + this.stopTime);

      this.elapsedTime = this.stopTime - this.startTime;

      console.log('the elapsed time is: ' + this.elapsedTime + 'ms');

      this.timerService.createTask({
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
