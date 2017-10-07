import { Component, OnInit } from '@angular/core';
import {TimerService} from '../timer.service';
import {Task} from '../Model/task.model';

@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.css']
})
export class ReportComponent implements OnInit {

  tasks: Task[];

  constructor(private timerService: TimerService) { }

  ngOnInit() {
    this.timerService.getTasks().subscribe(
      response => this.tasks = response.json()
    );
  }
}
