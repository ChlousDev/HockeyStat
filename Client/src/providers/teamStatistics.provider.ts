import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Rx';

import { environment } from '../environments/environment';

import { ApiErrorHandlingProvider } from './apiErrorHandling.provider';

import { TeamStatistics } from '../model/teamStatistics';

@Injectable()
export class TeamStatisticsProvider {

    private teamStatisticsApiUrl: string;

    constructor(private http: Http, private errorHandling: ApiErrorHandlingProvider) {
        this.teamStatisticsApiUrl = environment.apiEndpoint + 'teamStatistics';
    }

    public getTeamStatistics(seasonID: number, teamID: number): Observable<TeamStatistics> {
        return this.http.get(this.teamStatisticsApiUrl + '/season/' + seasonID + '/team/' + teamID)
            .map((response) => response.json())
            ._catch(this.errorHandling.handleError);
    }
}