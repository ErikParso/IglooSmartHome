import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { Store } from '@ngrx/store';
import * as reducer from './reactive-state/app.reducer';
import * as actions from './reactive-state/app.actions';
import { OidcSecurityService } from 'angular-auth-oidc-client';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  public isAuthenticated$: Observable<boolean>;

  constructor(private store: Store<reducer.AppState>, private oidcSecurityService: OidcSecurityService) {

  }

  ngOnInit(): void {
    this.isAuthenticated$ = this.store.select(reducer.isAuthenticatedSelector);
    this.store.dispatch(actions.authenticate());
  }

  login(): void {
    this.oidcSecurityService.authorize();
  }

  logout(): void {
    this.oidcSecurityService.logoff();
  }
}
