export interface LoginRequest {
  email: string,
  password: string
}

export class LoginRequestImpl implements LoginRequest {
  public email: string;
  public password: string;

  constructor(email: string, password: string) {
    this.email = email;
    this.password = password;
  }
}
