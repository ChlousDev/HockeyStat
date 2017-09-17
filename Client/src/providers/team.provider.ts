import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Rx';

import { environment } from '../environments/environment';

import { ApiErrorHandlingProvider } from './apiErrorHandling.provider';

import { Team } from '../model/team';

@Injectable()
export class TeamProvider {

    private teamApiUrl: string;

    constructor(private http: Http, private errorHandling: ApiErrorHandlingProvider) {
        this.teamApiUrl = environment.apiEndpoint + 'team';
    }

    public getTeams(): Observable<Team[]> {
        return this.http.get(this.teamApiUrl)
            .map((response) => response.json())
            ._catch(this.errorHandling.handleError);
    }

    public saveTeam(team: Team): Observable<void> {
        var result: Observable<void> = null;
        if (team.id > 0) {
            result = this.http.put(this.teamApiUrl, team, { withCredentials: true})
                ._catch(this.errorHandling.handleError);
        }
        else {
            result = this.http.post(this.teamApiUrl, team, { withCredentials: true})
                ._catch(this.errorHandling.handleError);
        }
        return result;
    }
}