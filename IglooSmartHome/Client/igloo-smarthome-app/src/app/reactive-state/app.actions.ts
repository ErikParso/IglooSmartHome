import { createAction, props } from '@ngrx/store';

export const loadVersion = createAction(
    '[App] Load version'
);

export const loadVersionSuccess = createAction(
    '[App] Load version success',
    props<{ version: string }>()
);

export const loadVersionError = createAction(
    '[App] Load version error',
    props<{ error: Error }>()
);
