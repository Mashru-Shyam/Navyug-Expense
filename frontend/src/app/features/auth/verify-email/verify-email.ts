import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { AuthService } from '../auth-service';

@Component({
  selector: 'app-verify-email',
  standalone: true,
  imports: [RouterModule, CommonModule],
  templateUrl: './verify-email.html',
  styleUrl: './verify-email.css',
})
export class VerifyEmail implements OnInit {
  status: 'loading' | 'success' | 'error' = 'loading';

  constructor(private route: ActivatedRoute, private router: Router, private authService: AuthService) { }

  ngOnInit(): void {
    this.route.queryParamMap.subscribe(params => {
      const token = params.get('token');
      console.log('Token from URL:', token);

      if (!token) {
        this.status = 'error';
        return;
      }

      this.authService.verifyEmail(token).subscribe({
        next: (response) => {
          console.log('Email verification successful:', response);
          this.status = 'success';

          setTimeout(() => {
            this.router.navigate(['/auth/login']);
          }, 3000);
        },
        error: err => {
          this.status = 'error';
        }
      });
    });
  }
}

