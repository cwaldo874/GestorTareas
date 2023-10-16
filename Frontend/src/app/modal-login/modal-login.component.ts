import { Component } from '@angular/core';
import { User } from '../model/user';
import { UserService } from '../services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-modal-login',
  templateUrl: './modal-login.component.html',
  styleUrls: ['./modal-login.component.css']
})
export class ModalLoginComponent {
  user: User = new User();
  token: string;

  constructor(private userService: UserService, private router: Router) { }


  login() {
    if (!this.validateEmailFormat(this.user.email)) {
      return;
    }
    if (this.user.password == undefined || this.user.password == "")
      return;
    this.generateToken(this.user);
  }

  validateEmailFormat(email: string): boolean {
    if (this.user.email == undefined)
      return false;
    var re = new RegExp("[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$");
    return re.test(email);
  }

  generateToken(user: User): void {
    this.userService.getTokenUser(user).subscribe((data: any) => {
      this.token = data;
      localStorage.setItem('Token', JSON.stringify(this.token));
      this.router.navigate(['task-list']);
    }
    );
  }

}
