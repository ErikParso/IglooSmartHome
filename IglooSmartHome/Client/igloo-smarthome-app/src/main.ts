import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';

declare var cordova: any;

(window as any).handleOpenURL = (url) => {
  const event = new CustomEvent('urlOpen', { detail: url });
  window.dispatchEvent(event);
}

if (environment.production) {
  enableProdMode();
}

let onDeviceReady = () => {
  platformBrowserDynamic().bootstrapModule(AppModule);
};
document.addEventListener('deviceready', onDeviceReady, false);
