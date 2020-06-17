import { createAction, props } from '@ngrx/store';
import { User } from 'oidc-client';

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

export const authenticate = createAction('[App] Authenticate');
export const authenticateSuccess = createAction('[App] Authenticate success', props<{ isAuthenticated: boolean, token: string, userData: any }>());
