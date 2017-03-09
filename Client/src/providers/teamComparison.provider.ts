import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Rx';

import { APP_CONFIG } from '../app/app.config.token';
import { IAppConfig } from '../app/app.config.interface';

import { ApiErrorHandlingProvider } from './apiErrorHandling.provider';

import { TeamComparison } from '../model/teamcomparison';

@Injectable()
export class TeamComparisonProvider {

    private comparisonApiUrl: string;

    constructor(private http: Http, @Inject(APP_CONFIG) private config: IAppConfig, private errorHandling: ApiErrorHandlingProvider) {
        this.comparisonApiUrl = this.config.apiEndpoint + 'teamComparison';
    }

    public getTeamComparison(seasonID: number, team1ID: number, team2ID: number): Observable<TeamComparison> {
        return this.http.get(this.comparisonApiUrl + '/season/' + seasonID + '/team1/' + team1ID + '/team2/' + team2ID)
            .map((response) => response.json())
            ._catch(this.errorHandling.handleError);
    }
}