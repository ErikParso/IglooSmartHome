import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: 'smarthome',
        loadChildren: () => import('./smarthome/smarthome.module').then(mod => mod.SmarthomeModule)
    },
    {
        path: '',
        redirectTo: 'smarthome',
        pathMatch: 'full'
    }
];