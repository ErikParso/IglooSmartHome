import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { Store } from '@ngrx/store';
import * as reducer from './reactive-state/app.reducer';
import * as actions from './reactive-state/app.actions';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  public version$: Observable<string>;

  constructor(private store: Store<reducer.AppState>) {
    
  }

  ngOnInit(): void {
    this.version$ = this.store.select(reducer.versionSelector);
    this.store.dispatch(actions.loadVersion());
  }
}
