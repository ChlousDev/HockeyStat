import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Rx';

import { APP_CONFIG } from '../app/app.config.token';
import { IAppConfig } from '../app/app.config.interface';

import { ApiErrorHandlingProvider } from './apiErrorHandling.provider';

import { Standings } from '../model/standings';

@Injectable()
export class StandingsProvider {

    private standingsApiUrl: string;

    constructor(private http: Http, @Inject(APP_CONFIG) private config: IAppConfig, private errorHandling: ApiErrorHandlingProvider) {
        this.standingsApiUrl = this.config.apiEndpoint + 'standings';
    }

    public getStandings(seasonID: number, date: Date): Observable<Standings> {
        return this.http.get(this.standingsApiUrl + '/season/' + seasonID + '/date/' + date.toISOString())
            .map((response) => response.json())
            ._catch(this.errorHandling.handleError);
    }
}