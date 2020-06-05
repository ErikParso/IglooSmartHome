import { createSelector, createReducer, on, ActionReducerMap } from '@ngrx/store';
import * as actions from './app.actions';

export interface AppState {
    versionInfo: VersionInfo;
}

export interface VersionInfo {
    version: string;
    loading: boolean;
    error: Error;
}

export const initialVersionInfo: VersionInfo = {
    version: "",
    loading: false,
    error: null
};

export const versionInfoSelector = (state: AppState) => state.versionInfo;
export const versionSelector = createSelector(
    versionInfoSelector,
    versionInfo => versionInfo.version
);

const reducerVersionInfo = createReducer<VersionInfo>(
    initialVersionInfo,
    on(actions.loadVersion, state => ({ ...state, loading: true })),
    on(actions.loadVersionSuccess, (state, { version }) => ({ ...state, loading: false, version })),
    on(actions.loadVersionError, (state, { error }) => ({ ...state, loading: false, error }))
);

export const reducers: ActionReducerMap<AppState> = {
    versionInfo: reducerVersionInfo
};