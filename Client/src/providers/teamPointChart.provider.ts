import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Rx';

import { APP_CONFIG } from '../app/app.config.token';
import { IAppConfig } from '../app/app.config.interface';

import { ApiErrorHandlingProvider } from './apiErrorHandling.provider';

import { TeamPointChart } from '../model/teamPointChart';

@Injectable()
export class TeamPointChartProvider {

    private teamPointChartApiUrl: string;

    constructor(private http: Http, @Inject(APP_CONFIG) private config: IAppConfig, private errorHandling: ApiErrorHandlingProvider) {
        this.teamPointChartApiUrl = this.config.apiEndpoint + 'teamPointChart';
    }

    public getTeamPointChart(seasonID: number): Observable<TeamPointChart> {
        return this.http.get(this.teamPointChartApiUrl + '/season/' + seasonID)
            .map((response) => response.json())
            ._catch(this.errorHandling.handleError);
    }
}