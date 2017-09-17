import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Rx';

import { environment } from '../environments/environment';

import { ApiErrorHandlingProvider } from './apiErrorHandling.provider';

import { Standings } from '../model/standings';

@Injectable()
export class StandingsProvider {

    private standingsApiUrl: string;

    constructor(private http: Http, private errorHandling: ApiErrorHandlingProvider) {
        this.standingsApiUrl = environment.apiEndpoint + 'standings';
    }

    public getStandings(seasonID: number, date: Date): Observable<Standings> {
        return this.http.get(this.standingsApiUrl + '/season/' + seasonID + '/date/' + date.toISOString())
            .map((response) => response.json())
            ._catch(this.errorHandling.handleError);
    }
}