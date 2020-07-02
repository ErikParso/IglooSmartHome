import { LogLevel } from 'angular-auth-oidc-client';

export const environment = {
  production: true,
  oidcClient: {
    identityServerUrl: 'http://igloo-identityserver-app.azurewebsites.net/',
    logLevel: LogLevel.Debug,
  },
  smartHomeApiUrl: 'http://igloo-identityserver-app.azurewebsites.net/'
};
