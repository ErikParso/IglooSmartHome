import { Component, NgZone } from '@angular/core';
import { Observable } from 'rxjs';
import { Store } from '@ngrx/store';
import * as reducer from './reactive-state/app.reducer';
import * as actions from './reactive-state/app.actions';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { UserManager, UserManagerSettings, User } from 'oidc-client';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  private config: UserManagerSettings = {
    authority: environment.oidcClient.identityServerUrl,
    redirect_uri: "igloosmarthomeapp://",
    popup_redirect_uri: "igloosmarthomeapp://",
    post_logout_redirect_uri: `igloosmarthomeapp://`,
    popup_post_logout_redirect_uri: "igloosmarthomeapp://",
    client_id: 'iglooSmartHomeClient',
    scope: 'openid profile email iglooSmartHomeApi',
    response_type: 'code',
  }

  private userManager: UserManager;

  public isAuthenticated$: Observable<boolean>;
  public user: User;

  constructor(
    private zone: NgZone,
    private store: Store<reducer.AppState>,
    private oidcSecurityService: OidcSecurityService) {

    this.userManager = new UserManager(this.config);
  }

  ngOnInit(): void {
    console.log("AppComponent init, adding url handler");
    window.addEventListener("urlOpen", (e: CustomEvent) => {
      var search = new URL(e.detail).searchParams;
      if (search.has("code"))
        this.userManager.signinRedirectCallback(e.detail)
          .then(user => this.zone.run(() => this.user = user));
      else
        this.userManager.signoutRedirectCallback(e.detail)
          .then(response => {
            console.log("signout callback response", response);
            this.userManager.getUser()
              .then(user => console.log("after logout callback user", user));
              this.zone.run(() => this.user = null);
          })
    })
  }

  login(): void {
    this.userManager.signinRedirect();
  }

  logout(): void {
    this.userManager.signoutRedirect();
  }
}
