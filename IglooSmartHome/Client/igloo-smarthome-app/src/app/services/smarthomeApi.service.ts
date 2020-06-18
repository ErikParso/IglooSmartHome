import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import { environment } from 'src/environments/environment';
import { AppState, tokenSelector } from '../reactive-state/app.reducer';


@Injectable({
    providedIn: 'root'
})
export class SmarthomeApiService {

    private token: string = "";

    constructor(private httpClient: HttpClient, private store: Store<AppState>) {
        this.store.select(tokenSelector).subscribe(token => {
            this.token = token;
        })
    }

    getUserInfo = () =>
        this.httpClient.get<any>(environment.smartHomeApiUrl + "/api/user", this.getHttpOptions());

    private getHttpOptions = () => ({
        headers: new HttpHeaders({
            Authorization: 'Bearer ' + this.token,
        }),
    });
}
