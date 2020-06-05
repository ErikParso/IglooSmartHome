import { Injectable } from '@angular/core';
import { Observable, interval, timer, of } from 'rxjs';
import { map, timeout, delay } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class VersionService {
    public getServiceVersions(): Observable<string> {
        return of("1.1.0").pipe(delay(10_000))
    }
}