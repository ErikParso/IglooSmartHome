import { BrowserModule } from '@angular/platform-browser';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { AppRoutingModule } from '@src/app/app-routing.module';
import { AppComponent } from '@src/app/app.component';
import { StoreModule } from '@ngrx/store';
import { reducers } from '@src/app/reactive-state/app.reducer';
import { EffectsModule } from '@ngrx/effects';
import { AppEffects } from '@src/app/reactive-state/app.effects';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { HttpClientModule } from '@angular/common/http';
import { AuthModule, LogLevel, OidcConfigService, OidcSecurityService } from 'angular-auth-oidc-client';
import { environment } from 'src/environments/environment';
import { AutoGeneratedComponent } from '@src/app/auto-generated/auto-generated.component';

export function configureAuth(oidcConfigService: OidcConfigService) {
  return () =>
      oidcConfigService.withConfig({
          stsServer: environment.oidcClient.identityServerUrl,
          redirectUrl: `${window.location.origin}/smarthome`,
          postLogoutRedirectUri: `${window.location.origin}/smarthome`,
          clientId: 'iglooSmartHomeClient',
          scope: 'openid profile email iglooSmartHomeApi',
          responseType: 'code',
          logLevel: environment.oidcClient.logLevel,
      });
}

@NgModule({
  declarations: [
    AppComponent,
    AutoGeneratedComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    AuthModule.forRoot(),
    StoreModule.forRoot(reducers),
    EffectsModule.forRoot([AppEffects]),
    StoreDevtoolsModule.instrument()
  ],
  providers: [
    OidcConfigService,
        {
            provide: APP_INITIALIZER,
            useFactory: configureAuth,
            deps: [OidcConfigService],
            multi: true,
        },
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(private oidcSecurityService: OidcSecurityService) {

  }
}
