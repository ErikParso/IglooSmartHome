import { createAction, props } from '@ngrx/store';

export const authenticate = createAction('[App] Authenticate');
export const authenticateSuccess = createAction('[App] Authenticate success', props<{ token: string }>());
export const authenticateFailed = createAction('[App] Authenticate failed');
export const setUserInfo = createAction('[App] Set user info', props<{userInfo: any}>());
