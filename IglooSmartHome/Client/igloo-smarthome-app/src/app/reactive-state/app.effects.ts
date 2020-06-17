import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { map, mergeMap, catchError, flatMap } from 'rxjs/operators';
import { VersionService } from '../services/version.service';
import * as actions from './app.actions';
import { of } from 'rxjs';
import { OidcSecurityService } from 'angular-auth-oidc-client';

@Injectable()
export class AppEffects {
  loadVersions$ = createEffect(() => this.actions$.pipe(
    ofType(actions.loadVersion),
    mergeMap(() => this.versionService.getServiceVersions()
      .pipe(
        map(version => actions.loadVersionSuccess({ version })),
        catchError(error => of(actions.loadVersionError({ error })))
      ))
  ));

  authenticate$ = createEffect(() => this.actions$.pipe(
    ofType(actions.authenticate),
    mergeMap(() => this.oidcSecurityService.checkAuth()
      .pipe(mergeMap(isAuthenticated => this.oidcSecurityService.userData$
        .pipe(map(userData =>
          actions.authenticateSuccess({
            isAuthenticated,
            token: this.oidcSecurityService.getToken(),
            userData
          }))))))
  ))

  constructor(
    private actions$: Actions,
    private versionService: VersionService,
    private oidcSecurityService: OidcSecurityService
  ) { }
}
