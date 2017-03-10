import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Rx';

import { APP_CONFIG } from '../app/app.config.token';
import { IAppConfig } from '../app/app.config.interface';

import { ApiErrorHandlingProvider } from './apiErrorHandling.provider';

import { TeamStatistics } from '../model/teamStatistics';

@Injectable()
export class TeamStatisticsProvider {

    private comparisonApiUrl: string;

    constructor(private http: Http, @Inject(APP_CONFIG) private config: IAppConfig, private errorHandling: ApiErrorHandlingProvider) {
        this.comparisonApiUrl = this.config.apiEndpoint + 'teamStatistics';
    }

    public getTeamComparison(seasonID: number, teamID: number): Observable<TeamStatistics> {
        return this.http.get(this.comparisonApiUrl + '/season/' + seasonID + '/team/' + teamID)
            .map((response) => response.json())
            ._catch(this.errorHandling.handleError);
    }
}