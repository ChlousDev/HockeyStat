import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Rx';

import { environment } from '../environments/environment';

import { ApiErrorHandlingProvider } from './apiErrorHandling.provider';

import { Season } from '../model/season';

@Injectable()
export class SeasonProvider {

    private seasonApiUrl: string;

    constructor(private http: Http, private errorHandling: ApiErrorHandlingProvider) {
        this.seasonApiUrl = environment.apiEndpoint + 'season';
    }

    public getSeasons(): Observable<Season[]> {
        return this.http.get(this.seasonApiUrl)
            .map((response) => response.json())
            ._catch(this.errorHandling.handleError);
    }

    public saveSeason(season: Season): Observable<void> {
        var result: Observable<void> = null;
        if (season.id > 0) {
            result = this.http.put(this.seasonApiUrl, season, { withCredentials: true})
                ._catch(this.errorHandling.handleError);
        }
        else {
            result = this.http.post(this.seasonApiUrl, season, { withCredentials: true})
                ._catch(this.errorHandling.handleError);
        }
        return result;
    }
}