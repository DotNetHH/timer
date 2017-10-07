import { Injectable } from '@angular/core';
import {Http, Headers, RequestOptions} from '@angular/http';
import {Task} from './Model/task.model';

@Injectable()
export class TimerService {

  private url = 'http://localhost:5000/api/tasks';
  headers;
  options;

  constructor(private http: Http) {
  this.headers = new Headers({ 'Content-Type': 'application/json' });
  this.options = new RequestOptions({ headers: this.headers });
}

  getTasks() {
    return this.http.get(this.url);
  }

  createTask(task: any) {
    this.http.post(this.url + '/stop', JSON.stringify(task), this.options).subscribe((response) => {
      // post.['id'] = respone.json().id;
      // this.posts.splice(0, 0, post);
      console.log(response);
    });
  }
}
