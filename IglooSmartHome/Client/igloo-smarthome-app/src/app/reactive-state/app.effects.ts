import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { map, mergeMap } from 'rxjs/operators';
import { SmarthomeApiService } from '../services/smarthomeApi.service';
import * as actions from './app.actions';

@Injectable()
export class AppEffects {

  authenticate$ = createEffect(() => this.actions$.pipe(
    ofType(actions.authenticate),
    mergeMap(() => this.oidcSecurityService.checkAuth()
      .pipe(map(isAuthenticated => isAuthenticated
        ? actions.authenticateSuccess({ token: this.oidcSecurityService.getToken(), })
        : actions.authenticateFailed())))
  ))

  // authenticateSucess$ = createEffect(() => this.actions$.pipe(
  //   ofType(actions.authenticateSuccess),
  //   mergeMap(() => this.smarthomeApiService.getUserInfo()
  //     .pipe(map(userInfo => actions.setUserInfo({ userInfo }))))
  // ))

  constructor(
    private actions$: Actions,
    private oidcSecurityService: OidcSecurityService,
    private smarthomeApiService: SmarthomeApiService,
  ) { }
}
