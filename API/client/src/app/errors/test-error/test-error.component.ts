import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {RegisterUser} from "../../_models/registerUser";

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.css']
})

export class TestErrorComponent implements OnInit {
  baseUrl = 'https://localhost:5001/api/';
  registerUser: RegisterUser = { username: 'test', password: 'n' };
  validationErrors: string[] = [];

  constructor(private http: HttpClient) { }

  ngOnInit() {
  }

  get404Error() {
    this.http.get(this.baseUrl + 'buggy/not-found').subscribe({
      next: response => console.log(response),
      error: err => console.log(err),
    })
  }

  get500Error() {
    this.http.get(this.baseUrl + 'buggy/server-error').subscribe({
      next: response => console.log(response),
      error: err => console.log(err),
    })
  }

  get400Error() {
    this.http.get(this.baseUrl + 'buggy/bad-request').subscribe({
      next: response => console.log(response),
      error: err => console.log(err),
    })
  }

  get401Error() {
    this.http.get(this.baseUrl + 'buggy/auth').subscribe({
      next: response => console.log(response),
      error: err => console.log(err),
    })
  }

  get400ValidationError() {
    this.http.post(this.baseUrl + 'Account/register', this.registerUser).subscribe({
      next: response => console.log(response),
      error: err => {
        console.log(err);
        this.validationErrors = err;
        },
    })
  }
}
