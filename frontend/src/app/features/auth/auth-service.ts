import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AuthService {

  constructor(private http: HttpClient) { }

  RegisterUser(email: string, password: string) {
    return this.http.post('/Auth/register', { email, password });
  }

  verifyEmail(token: string) {
    return this.http.post('/Auth/verify-email', { token });
  }

}
