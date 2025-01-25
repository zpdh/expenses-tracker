import {Component} from '@angular/core';
import {AuthService} from '../../core/auth/auth.service';
import {Router, RouterLink} from '@angular/router';
import {RegisterRequest} from '../../core/communication/register/registerRequest';
import {FormsModule} from '@angular/forms';

@Component({
  selector: 'app-register',
  imports: [
    FormsModule,
    RouterLink
  ],
  templateUrl: './register.component.html',
  standalone: true,
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  private readonly authService: AuthService;
  private readonly router: Router;

  protected name = '';
  protected email = '';
  protected password = '';

  constructor(authService: AuthService, router: Router) {
    this.authService = authService;
    this.router = router;
  }

  protected onSubmit() {
    const request: RegisterRequest = {
      name: this.name,
      email: this.email,
      password: this.password,
    }

    this.authService.register(request).subscribe({
      next: () => this.router.navigate(["login"]).then(),
      error: (err: Error) => alert(`Registration Failed: ${err.message}.`)
    })
  }
}
