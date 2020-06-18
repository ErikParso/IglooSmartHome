import { LogLevel } from 'angular-auth-oidc-client';

export const environment = {
  production: true,
  oidcClient: {
    identityServerUrl: '__indentity_server_url__',
    logLevel: LogLevel.Error,
  },
  smartHomeApiUrl: '__smarthome_api_url__'
};
