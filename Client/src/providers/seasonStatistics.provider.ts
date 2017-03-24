import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Rx';

import { APP_CONFIG } from '../app/app.config.token';
import { IAppConfig } from '../app/app.config.interface';

import { ApiErrorHandlingProvider } from './apiErrorHandling.provider';

import { SeasonStatistics } from '../model/seasonStatistics';

@Injectable()
export class SeasonStatisticsProvider {

    private teamStatisticsApiUrl: string;

    constructor(private http: Http, @Inject(APP_CONFIG) private config: IAppConfig, private errorHandling: ApiErrorHandlingProvider) {
        this.teamStatisticsApiUrl = this.config.apiEndpoint + 'seasonStatistics';
    }

    public getSeasonStatistics(seasonID: number): Observable<SeasonStatistics> {
        return this.http.get(this.teamStatisticsApiUrl + '/season/' + seasonID)
            .map((response) => response.json())
            ._catch(this.errorHandling.handleError);
    }
}