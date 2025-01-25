import {Component} from '@angular/core';
import {AuthService} from '../../../core/auth/auth.service';
import {Router, RouterLink} from '@angular/router';
import {NgIf} from '@angular/common';

@Component({
  selector: 'app-header',
  imports: [
    RouterLink,
    NgIf
  ],
  templateUrl: './header.component.html',
  standalone: true,
})
export class HeaderComponent {
  private readonly authService: AuthService;
  private readonly router: Router;

  protected isUnauthorizedPage = false;

  constructor(authService: AuthService, router: Router) {
    this.router = router;
    this.authService = authService;

    this.router.events.subscribe(
      () => this.isUnauthorizedPage = this.router.url === "/login" || this.router.url === "/register");
  }

  protected logout() {
    this.authService.logout();
    this.router.navigate(["login"]).then();
  }
}
