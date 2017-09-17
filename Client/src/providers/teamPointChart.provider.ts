import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Rx';

import { environment } from '../environments/environment';

import { ApiErrorHandlingProvider } from './apiErrorHandling.provider';

import { TeamPointChart } from '../model/teamPointChart';

@Injectable()
export class TeamPointChartProvider {

    private teamPointChartApiUrl: string;

    constructor(private http: Http, private errorHandling: ApiErrorHandlingProvider) {
        this.teamPointChartApiUrl = environment.apiEndpoint + 'teamPointChart';
    }

    public getTeamPointChart(seasonID: number): Observable<TeamPointChart> {
        return this.http.get(this.teamPointChartApiUrl + '/season/' + seasonID)
            .map((response) => response.json())
            ._catch(this.errorHandling.handleError);
    }
}