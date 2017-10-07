import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';
import { Http, RequestOptions, Headers } from '@angular/http';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/toPromise';

@Component({
  selector: 'page-home',
  templateUrl: 'home.html'
})
export class HomePage {

  constructor(public navCtrl: NavController, public http: Http) {

  }

  private handleError(error: any): Promise<any> {
    console.error('An error occurred', error); // for demo purposes only
    return Promise.reject(error.message || error);
  }

  stopTapped() {
    console.log('stop');

    // public DateTime DateTimeUtc { get; set; }
    // public string Ticket { get; set; }
    // public string Description { get; set; }

    let time = new Date();
    let ticket = "42";
    let description = "hooooooooah";
    let body = JSON.stringify({DateTimeUtc: time, ticket:ticket, description: description});
    let head = new Headers({'Content-Type': 'application/json'});

    this.http
      .post('http://localhost:50682/api/tasks/stop', 
        body, { headers: head } )
      .toPromise()
      .then(res => {
        
        console.log(res.json().data);
      
      })
      .catch(this.handleError);

  }


  startTapped() {
    console.log('start');

  }

}
