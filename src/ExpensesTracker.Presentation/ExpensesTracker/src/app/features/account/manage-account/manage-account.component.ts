import {Component, OnInit} from '@angular/core';
import {GetUserByIdResponse} from '../../../core/communication/user/get/getUserByIdResponse';
import {FormControl, ReactiveFormsModule} from '@angular/forms';
import {UserService} from '../../../core/services/user.service';
import {Router} from '@angular/router';
import {NgIf} from '@angular/common';

@Component({
  selector: 'app-manage-account',
  imports: [
    NgIf,
    ReactiveFormsModule
  ],
  templateUrl: './manage-account.component.html',
  standalone: true,
})
export class ManageAccountComponent implements OnInit {
  private readonly userService: UserService;
  private readonly router: Router;

  protected userData!: GetUserByIdResponse;
  protected nameControl = new FormControl({value: "", disabled: true});
  protected emailControl = new FormControl({value: "", disabled: true});
  protected errorMessage = "";


  constructor(userService: UserService, router: Router) {
    this.userService = userService;
    this.router = router;
  }

  ngOnInit(): void {
    this.userService.getUser().subscribe({
      next: (data: GetUserByIdResponse) => {
        this.userData = data;
        this.nameControl.setValue(data.name);
        this.emailControl.setValue(data.email);
      }
    });
  }

  protected deleteAccount(): void {
    if (confirm("Are you sure you want to delete your account?")) {
      this.userService.deleteUser().subscribe({
        next: () => {
          localStorage.clear();
          this.router.navigate(["login"]).then();
        },
        error: (err: Error) => {
          this.errorMessage = `Failed to delete account. Error: ${err.message}`;
        }
      });
    }
  }
}
