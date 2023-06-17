import {Component, OnInit} from '@angular/core';
import {AccountService} from "../_services/account.service";
import { BsDropdownConfig } from 'ngx-bootstrap/dropdown';
import {User} from "../_models/user";
import {Router} from "@angular/router";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
  providers: [{ provide: BsDropdownConfig, useValue: { isAnimated: true, autoClose: true }}]
})

export class NavComponent implements OnInit {
  model: any = {}
  user: User | any;
  constructor(
    public accountService: AccountService,
    private router: Router,
    private toastr: ToastrService) {}

  ngOnInit() {
  }

  login() {
    this.accountService.login(this.model).subscribe({
      next: () => this.router.navigateByUrl('/members'),
      error: err => this.toastr.error(err.error),
    })
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }
}
