import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { map, mergeMap, catchError } from 'rxjs/operators';
import { VersionService } from '../services/version.service';
import * as actions from './app.actions';
import { of } from 'rxjs';

@Injectable()
export class AppEffects {
  loadVersions$ = createEffect(() => this.actions$.pipe(
    ofType(actions.loadVersion),
    mergeMap(() => this.versionService.getServiceVersions()
      .pipe(
        map(version => actions.loadVersionSuccess({ version })),
        catchError(error => of(actions.loadVersionError({error})))
      ))
  ));

  constructor(
    private actions$: Actions,
    private versionService: VersionService
  ) { }
}
