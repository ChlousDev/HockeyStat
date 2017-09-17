import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Rx';

import { environment } from '../environments/environment';

import { ApiErrorHandlingProvider } from './apiErrorHandling.provider';

import { TeamComparison } from '../model/teamComparison';

@Injectable()
export class TeamComparisonProvider {

    private teamComparisonApiUrl: string;

    constructor(private http: Http, private errorHandling: ApiErrorHandlingProvider) {
        this.teamComparisonApiUrl = environment.apiEndpoint + 'teamComparison';
    }

    public getTeamComparison(seasonID: number, team1ID: number, team2ID: number): Observable<TeamComparison> {
        return this.http.get(this.teamComparisonApiUrl + '/season/' + seasonID + '/team1/' + team1ID + '/team2/' + team2ID)
            .map((response) => response.json())
            ._catch(this.errorHandling.handleError);
    }
}