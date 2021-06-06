import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/Auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
})
export class NavComponent implements OnInit {
  model: any = {};

  constructor(private authService:AuthService) {}

  ngOnInit() {}

  login() {
    this.authService.login(this.model).subscribe(next=>{
      console.log("login success");
    },error=>{
      console.log("login failed");
    })
  }

  loggedin(){
    const token= localStorage.getItem('token');
    return !!token;
  }

  logout(){
    localStorage.removeItem('token');
    console.log("logged out");
  }
}
