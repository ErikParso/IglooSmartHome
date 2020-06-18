import { ActionReducerMap, createReducer, createSelector, on } from '@ngrx/store';
import * as actions from './app.actions';

export interface AppState {
    userInfo: UserInfo;
}

export interface UserInfo {
    isAuthenticated: boolean;
    token: string;
    userInfo: any;
}

export const initialUserInfo: UserInfo = {
    isAuthenticated: false,
    token: null,
    userInfo: null
};

export const userInfoSelector = (state: AppState) => state.userInfo;
export const isAuthenticatedSelector = createSelector(
    userInfoSelector,
    userInfo => userInfo.isAuthenticated
);
export const tokenSelector = createSelector(
    userInfoSelector,
    userInfo => userInfo.token,
);

const reduceUserInfo = createReducer<UserInfo>(
    initialUserInfo,
    on(actions.authenticate, state => ({ ...state })),
    on(actions.authenticateSuccess, (state, { token }) => ({ ...state, isAuthenticated: true, token })),
    on(actions.setUserInfo, (state, { userInfo }) => ({ ...state, userInfo }))
);

export const reducers: ActionReducerMap<AppState> = {
    userInfo: reduceUserInfo
};
