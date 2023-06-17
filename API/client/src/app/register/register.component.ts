import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {AccountService} from "../_services/account.service";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})

export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter(); // for send info to parent component (home component)
  model: any = {}
  constructor(private accountService: AccountService, private toastr: ToastrService) { }

  ngOnInit() {
  }

  register() {
    this.accountService.register(this.model).subscribe({
      next: () => {
        this.cancel(); // for close register form
      },
      error: err => {
        this.toastr.error(err.error.errors)
      }
    })
  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}
