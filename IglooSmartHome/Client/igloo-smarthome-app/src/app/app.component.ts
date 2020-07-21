import { Component } from '@angular/core';
import { User } from 'oidc-client';
import { Observable } from 'rxjs';
import { AuthenticationService } from './services/authentication.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  public isAuthenticated$: Observable<boolean>;
  public user: User;

  constructor(private authenticationService: AuthenticationService) { }

  ngOnInit(): void {
    this.authenticationService.user$.subscribe(u => this.user = u);
  }

  login(): void {
    this.authenticationService.login();
  }

  logout(): void {
    this.authenticationService.logout();
  }
}
