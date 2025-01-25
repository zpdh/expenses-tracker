import {Component} from '@angular/core';
import {AuthService} from '../../core/auth/auth.service';
import {Router, RouterLink} from '@angular/router';
import {LoginRequestImpl} from '../../core/communication/login/loginRequest';
import {FormsModule} from '@angular/forms';

@Component({
  selector: 'app-login',
  imports: [
    FormsModule,
    RouterLink
  ],
  templateUrl: './login.component.html',
  standalone: true,
})
export class LoginComponent {
  private readonly authService: AuthService;
  private readonly router: Router;

  protected email = "";
  protected password = "";

  constructor(authService: AuthService, router: Router) {
    this.authService = authService;
    this.router = router;
  }

  protected onSubmit() {
    const request = new LoginRequestImpl(this.email, this.password);

    this.authService.login(request).subscribe({
      next: () => this.router.navigate(['/expenses']),
      error: (err: Error) => alert(`Login fail: ${err.message}`)
    });
  }
}
