import { Injectable } from '@angular/core';
import { map, mergeMap, catchError, tap } from 'rxjs/operators';
import { createEffect, ofType, Actions } from '@ngrx/effects';
import * as actions from './app.actions';
import { createAction } from '@ngrx/store';
import { of } from 'rxjs';


@Injectable()
export class AppEffects {
    loadVersions$ = createEffect(() => this.actions$.pipe(
        ofType(actions.loadVersions),
        mergeMap(() => this.versionService.getServiceVersions()
          .pipe(
            map(versionInfo => actions.loadVersionsSuccess(versionInfo))
          ))
      ));

      constructor(
        private actions$: Actions
      ) { }
}