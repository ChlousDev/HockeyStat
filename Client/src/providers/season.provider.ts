import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Rx';

import { APP_CONFIG } from '../app/app.config.token';
import { IAppConfig } from '../app/app.config.interface';

import { ApiErrorHandlingProvider } from './apiErrorHandling.provider';

import { Season } from '../model/season';

@Injectable()
export class SeasonProvider {

    private seasonApiUrl: string;

    constructor(private http: Http, @Inject(APP_CONFIG) private config: IAppConfig, private errorHandling: ApiErrorHandlingProvider) {
        this.seasonApiUrl = this.config.apiEndpoint + 'season';
    }

    public getSeasons(): Observable<Season[]> {
        return this.http.get(this.seasonApiUrl)
            .map((response) => response.json())
            ._catch(this.errorHandling.handleError);
    }

    public saveSeason(season: Season): Observable<void> {
        var result: Observable<void> = null;
        if (season.ID > 0) {
            result = this.http.put(this.seasonApiUrl, season, { withCredentials: true})
                .map((response) => response.json())
                ._catch(this.errorHandling.handleError);
        }
        else {
            result = this.http.post(this.seasonApiUrl, season, { withCredentials: true})
                .map((response) => response.json())
                ._catch(this.errorHandling.handleError);
        }
        return result;
    }
}