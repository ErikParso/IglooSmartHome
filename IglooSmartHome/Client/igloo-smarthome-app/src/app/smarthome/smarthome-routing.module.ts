import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SmarthomeComponent } from './smarthome.component';

const routes: Routes = [{
  path: '',
  component: SmarthomeComponent,
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SmarthomeRoutingModule { }
