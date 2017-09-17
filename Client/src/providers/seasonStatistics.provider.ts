import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Rx';

import { environment } from '../environments/environment';

import { ApiErrorHandlingProvider } from './apiErrorHandling.provider';

import { SeasonStatistics } from '../model/seasonStatistics';

@Injectable()
export class SeasonStatisticsProvider {

    private teamStatisticsApiUrl: string;

    constructor(private http: Http, private errorHandling: ApiErrorHandlingProvider) {
        this.teamStatisticsApiUrl = environment.apiEndpoint + 'seasonStatistics';
    }

    public getSeasonStatistics(seasonID: number): Observable<SeasonStatistics> {
        return this.http.get(this.teamStatisticsApiUrl + '/season/' + seasonID)
            .map((response) => response.json())
            ._catch(this.errorHandling.handleError);
    }
}