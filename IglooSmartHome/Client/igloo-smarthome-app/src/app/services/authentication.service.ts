import { Injectable, NgZone } from '@angular/core';
import { User, UserManager, UserManagerSettings } from 'oidc-client';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
})
export class AuthenticationService {

    private config: UserManagerSettings = {
        authority: environment.oidcClient.identityServerUrl,
        redirect_uri: "igloosmarthomeapp://",
        popup_redirect_uri: "igloosmarthomeapp://",
        post_logout_redirect_uri: `igloosmarthomeapp://`,
        popup_post_logout_redirect_uri: "igloosmarthomeapp://",
        client_id: 'iglooSmartHomeClient',
        scope: 'openid profile email iglooSmartHomeApi',
        response_type: 'code',
    };

    private userManager: UserManager;

    private user: BehaviorSubject<User>;
    public user$: Observable<User>;

    constructor(
        private zone: NgZone
    ) {
        this.userManager = new UserManager(this.config);
        this.user = new BehaviorSubject<User>(null);
        this.user$ = this.user.asObservable();
        (window as any).handleOpenURL = this.handleOpenUrl;
    }

    public login = () =>
        this.userManager.signinRedirect();

    public logout = () => 
        this.userManager.signoutRedirect();

    private handleOpenUrl = (url: string) => {
        const search = new URL(url).searchParams;
        const promise = search.has("code")
            ? this.userManager.signinRedirectCallback(url)
            : this.userManager.signoutRedirectCallback(url).then(() => null as User);
        this.zone.run(() => promise.then(user => this.user.next(user)));
    };
}
