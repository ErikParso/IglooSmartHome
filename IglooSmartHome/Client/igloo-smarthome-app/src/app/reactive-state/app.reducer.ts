import { ActionReducerMap, createReducer, createSelector, on } from '@ngrx/store';
import * as actions from './app.actions';

export interface AppState {
    versionInfo: VersionInfo;
    userInfo: UserInfo;
}

export interface VersionInfo {
    version: string;
    loading: boolean;
    error: Error;
}

export interface UserInfo {
    isAuthenticated: boolean;
    token: string;
    userData: any;
}

export const initialVersionInfo: VersionInfo = {
    version: "",
    loading: false,
    error: null
};

export const initialUserInfo: UserInfo = {
    isAuthenticated: false,
    token: null,
    userData: null
};

export const versionInfoSelector = (state: AppState) => state.versionInfo;
export const versionSelector = createSelector(
    versionInfoSelector,
    versionInfo => versionInfo.version
);

export const userInfoSelector = (state: AppState) => state.userInfo;
export const isAuthenticatedSelector = createSelector(
    userInfoSelector,
    userInfo => userInfo.isAuthenticated
);

const reducerVersionInfo = createReducer<VersionInfo>(
    initialVersionInfo,
    on(actions.loadVersion, state => ({ ...state, loading: true })),
    on(actions.loadVersionSuccess, (state, { version }) => ({ ...state, loading: false, version })),
    on(actions.loadVersionError, (state, { error }) => ({ ...state, loading: false, error }))
);

const reduceUserInfo = createReducer<UserInfo>(
    initialUserInfo,
    on(actions.authenticate, state => ({ ...state })),
    on(actions.authenticateSuccess, (state, { isAuthenticated, token, userData }) => ({ ...state, isAuthenticated, token, userData })),
);

export const reducers: ActionReducerMap<AppState> = {
    versionInfo: reducerVersionInfo,
    userInfo: reduceUserInfo,
};